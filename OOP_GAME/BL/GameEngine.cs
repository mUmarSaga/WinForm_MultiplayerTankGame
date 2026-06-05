using OOP_GAME.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_GAME.BL
{
    public class GameEngine
    {
        // ─── dependencies ───────────────────────────────────────────
        private PhysicsEngine _physics;
        private TerrainManager _terrain;

        // ─── game objects ────────────────────────────────────────────
        public int[] Ground => _terrain.Ground;
        public List<Tank> Tanks { get; private set; }
        public Projectile ActiveProjectile { get; private set; }

        // ─── turn system ─────────────────────────────────────────────
        public int CurrentTurnIndex { get; private set; }
        public Tank CurrentTank => Tanks[CurrentTurnIndex];
        public bool IsPlayerTurn => !CurrentTank.IsAI;
        public int RoundNumber { get; private set; } = 1;
        public float WindStrength => _physics.WindStrength;

        // ─── state flags ─────────────────────────────────────────────
        public bool IsProjectileFlying { get; private set; }
        public bool IsGameOver { get; private set; }
        public Tank Winner { get; private set; }

        // ─── timer ───────────────────────────────────────────────────
        private Timer _gameTimer;
        public int PanelWidth { get; private set; }
        public int PanelHeight { get; private set; }

        // ─── events ──────────────────────────────────────────────────
        public event Action OnTick;
        public event Action<Tank> OnTurnChanged;
        public event Action<ImpactResult> OnProjectileHit;
        public event Action<Tank> OnTankDied;
        public event Action<Tank> OnGameOver;

        private List<Projectile> _activeSubProjectiles = new List<Projectile>();

        public GameEngine(int panelWidth, int panelHeight)
        {
            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            _physics = new PhysicsEngine();
            _terrain = new TerrainManager(panelWidth, panelHeight);
            Tanks = new List<Tank>();
        }

        // ─────────────────────────────────────────────────────────────
        //  SETUP
        // ─────────────────────────────────────────────────────────────

        public void StartGame(List<Tank> tanks)
        {
            Tanks = tanks;

            // generate terrain — Ground is now accessed via _terrain.Ground
            _terrain.GenerateTerrain();

            // flatten spawn points so tanks don't start on steep slopes
            int spacing = PanelWidth / (Tanks.Count + 1);
            var spawnPoints = new List<int>();
            for (int i = 0; i < Tanks.Count; i++)
                spawnPoints.Add(spacing * (i + 1));

            _terrain.FlattenSpawnPoints(spawnPoints);

            // place tanks on terrain
            PlaceTanksOnTerrain(spawnPoints);

            // give each tank weapons
            foreach (var tank in Tanks)
            {
                tank.WeaponInventory.Add(new Cannon());
                tank.WeaponInventory.Add(new ClusterBombWeapon());
                tank.CurrentWeapon = tank.WeaponInventory[0];
            }

            _physics.WindStrength = GenerateWind();

            _gameTimer = new Timer();
            _gameTimer.Interval = 16;
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();

            CurrentTurnIndex = 0;
            OnTurnChanged?.Invoke(CurrentTank);
        }

        private void PlaceTanksOnTerrain(List<int> spawnPoints)
        {
            for (int i = 0; i < Tanks.Count; i++)
            {
                int x = spawnPoints[i];
                Tanks[i].X = x;
                Tanks[i].Y = Ground[x] - Tanks[i].Height;
            }
        }

        private float GenerateWind()
        {
            var rng = new Random();
            return (float)(rng.NextDouble() * 0.16 - 0.08);
        }

        // ─────────────────────────────────────────────────────────────
        //  GAME LOOP
        // ─────────────────────────────────────────────────────────────

        private void GameLoop(object sender, EventArgs e)
        {
            if (IsGameOver) return;

            // update active projectile
            if (IsProjectileFlying && ActiveProjectile != null)
            {
                _physics.UpdateProjectile(ActiveProjectile);
                CheckProjectileImpact();
            }

            // update sub projectiles (cluster children)
            for (int i = _activeSubProjectiles.Count - 1; i >= 0; i--)
            {
                var sub = _activeSubProjectiles[i];
                _physics.UpdateProjectile(sub);
                ImpactResult subResult = _physics.CheckImpact(sub, Ground, Tanks);
                if (subResult != null)
                {
                    _terrain.CreateCrater(subResult.X, (int)sub.BlastRadius);
                    _physics.ApplySplashDamage(Tanks, subResult.X, subResult.Y,
                        (int)sub.BlastRadius, (int)sub.Damage);
                    sub.IsActive = false;
                    _activeSubProjectiles.RemoveAt(i);
                    CheckDeaths();
                }
            }

            // update all tanks
            foreach (var tank in Tanks)
            {
                if (tank.IsAlive)
                    _physics.UpdateTank(tank, Ground);
            }

            // AI turn
            if (!IsProjectileFlying && !IsGameOver && CurrentTank.IsAI
                && _activeSubProjectiles.Count == 0)
            {
                AiTakeTurn();
            }

            OnTick?.Invoke();
        }

        // ─────────────────────────────────────────────────────────────
        //  PROJECTILE
        // ─────────────────────────────────────────────────────────────

        private void CheckProjectileImpact()
        {
            ImpactResult result = _physics.CheckImpact(ActiveProjectile, Ground, Tanks);
            if (result == null) return;

            IsProjectileFlying = false;

            if (result.HitGround || result.HitTank != null)
            {
                // crater now via TerrainManager, not PhysicsEngine
                _terrain.CreateCrater(result.X, (int)ActiveProjectile.BlastRadius);

                _physics.ApplySplashDamage(
                    Tanks,
                    result.X, result.Y,
                    (int)ActiveProjectile.BlastRadius,
                    (int)ActiveProjectile.Damage
                );

                // cluster bomb splits into children
                if (ActiveProjectile is ClusterBomb cb && !cb.HasSplit)
                    SpawnClusters(cb);
            }

            OnProjectileHit?.Invoke(result);
            CheckDeaths();

            if (!IsGameOver)
                Task.Delay(1000).ContinueWith(_ => NextTurn());
        }

        private void SpawnClusters(ClusterBomb parent)
        {
            parent.HasSplit = true;
            for (int i = 0; i < parent.NumClusters; i++)
            {
                float angle = 20f + (i * 140f / parent.NumClusters);
                var child = new Bullet(parent.X, parent.Y, angle, 30f);
                child.OwnerId = parent.OwnerId;
                _activeSubProjectiles.Add(child);
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  TURN SYSTEM
        // ─────────────────────────────────────────────────────────────

        public void PlayerFire()
        {
            if (!IsPlayerTurn || IsProjectileFlying || IsGameOver) return;
            if (!CurrentTank.CurrentWeapon.CanFire()) return;

            float tipX, tipY;
            GetBarrelTip(CurrentTank, out tipX, out tipY);

            ActiveProjectile = CurrentTank.CurrentWeapon.CreateProjectile(
                tipX, tipY, CurrentTank.BarrelAngle, CurrentTank.FirePower);
            ActiveProjectile.OwnerId = CurrentTank.TeamId;
            CurrentTank.CurrentWeapon.UseAmmo();
            IsProjectileFlying = true;
        }

        public void MoveCurrentTank(float direction)
        {
            if (!IsPlayerTurn || IsProjectileFlying || IsGameOver) return;
            if (CurrentTank.Fuel <= 0) return;

            CurrentTank.X += direction * 3f; // just move X, physics snaps Y
            CurrentTank.Fuel -= 1;
        }

        public void AdjustBarrelAngle(float delta)
        {
            if (!IsPlayerTurn || IsProjectileFlying) return;
            float newAngle = CurrentTank.BarrelAngle + delta;
            CurrentTank.BarrelAngle = Math.Max(5f, Math.Min(newAngle, 175f));
        }

        public void AdjustFirePower(float delta)
        {
            if (!IsPlayerTurn || IsProjectileFlying) return;
            float newPower = CurrentTank.FirePower + delta;
            CurrentTank.FirePower = Math.Max(5f, Math.Min(newPower, 100f));
        }

        public void SwitchWeapon(int index)
        {
            if (!IsPlayerTurn || IsProjectileFlying) return;
            if (index < CurrentTank.WeaponInventory.Count)
                CurrentTank.CurrentWeapon = CurrentTank.WeaponInventory[index];
        }

        private void NextTurn()
        {
            if (IsGameOver) return;

            CurrentTank.Fuel = CurrentTank.MaxFuel;

            do
            {
                CurrentTurnIndex = (CurrentTurnIndex + 1) % Tanks.Count;
            }
            while (!CurrentTank.IsAlive);

            if (CurrentTurnIndex == 0)
            {
                RoundNumber++;
                _physics.WindStrength = GenerateWind();
            }

            ActiveProjectile = null;
            OnTurnChanged?.Invoke(CurrentTank);
        }
        private void GetBarrelTip(Tank tank, out float tipX, out float tipY)
        {
            float centerX = tank.X + tank.Width / 2f;
            float centerY = tank.Y + tank.Height / 3f; // same pivot as DrawBarrel

            double rad = tank.BarrelAngle * Math.PI / 180.0;
            float barrelLength = tank.Width / 2f;

            tipX = centerX + (float)(Math.Cos(rad) * barrelLength);
            tipY = centerY - (float)(Math.Sin(rad) * barrelLength); // minus = up
        }

        // ─────────────────────────────────────────────────────────────
        //  DEATH CHECK
        // ─────────────────────────────────────────────────────────────

        private void CheckDeaths()
        {
            foreach (var tank in Tanks)
            {
                if (tank.Health <= 0 && tank.IsAlive)
                {
                    // mark as dead by setting health to 0
                    tank.Health = 0;
                    OnTankDied?.Invoke(tank);
                }
            }

            // check if only one team left alive
            var aliveTeams = Tanks
                .Where(t => t.IsAlive)
                .Select(t => t.TeamId)
                .Distinct()
                .ToList();

            if (aliveTeams.Count == 1)
            {
                Winner = Tanks.First(t => t.IsAlive);
                IsGameOver = true;
                _gameTimer.Stop();
                OnGameOver?.Invoke(Winner);
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  AI
        // ─────────────────────────────────────────────────────────────

        private bool _aiIsThinking = false;

        private void AiTakeTurn()
        {
            if (_aiIsThinking) return;
            _aiIsThinking = true;

            var ai = CurrentTank as AiTank;
            if (ai == null) return;

            Tank target = Tanks.FirstOrDefault(t => t.IsAlive && t.TeamId != ai.TeamId);
            if (target == null) return;

            float dx = target.X - ai.X;
            float dy = ai.Y - target.Y;
            float perfectAngle = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);

            var rng = new Random();
            float error = (float)((1f - ai.AimAccuracy) * (rng.NextDouble() * 30 - 15));
            float newAngle = perfectAngle + error;
            ai.BarrelAngle = Math.Max(5f, Math.Min(newAngle, 175f));
            ai.FirePower = 60f + (float)(rng.NextDouble() * 20);

            Task.Delay(800).ContinueWith(_ =>
            {
                ActiveProjectile = ai.Fire();
                ActiveProjectile.OwnerId = ai.TeamId;
                ai.CurrentWeapon.UseAmmo();
                IsProjectileFlying = true;
                _aiIsThinking = false;
            });
        }

        // ─────────────────────────────────────────────────────────────
        //  CLEANUP
        // ─────────────────────────────────────────────────────────────

        public void StopGame()
        {
            _gameTimer?.Stop();
            _gameTimer?.Dispose();
        }
    }
}