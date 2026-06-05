using OOP_GAME.BL;
using OOP_GAME.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OOP_GAME.UI
{
    public partial class GameForm : Form
    {
        // ─── engine ──────────────────────────────────────────────────
        private GameEngine _engine;

        // ─── sprites ─────────────────────────────────────────────────
        private Image _Tank1Body;
        private Image _Tank2Body;
        private Image _Tank1Cannon;
        private Image _Tank2Cannon;
        private Image _aiBody;
        private Image _aiCannon;

        private bool _gameOver = false;
        private string _winnerName = "";

        // ─── HUD fonts/brushes ───────────────────────────────────────
        private Font _hudFont = new Font("Arial", 12, FontStyle.Bold);
        private Font _nameFont = new Font("Arial", 10, FontStyle.Bold);

        public GameForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
            

            // keyboard events
            this.KeyDown += GameForm_KeyDown;

            LoadSprites();
            this.Load += GameForm_Load;

        }

        // ─────────────────────────────────────────────────────────────
        //  LOAD SPRITES
        // ─────────────────────────────────────────────────────────────

        private void LoadSprites()
        {
            try
            {
                _Tank1Body = Image.FromFile(Model.CurrentSession.Player1BodyImage);
                _Tank1Cannon = Image.FromFile(Model.CurrentSession.Player1CannonImage);
                _Tank2Body = Image.FromFile(Model.CurrentSession.Player2BodyImage);
                _Tank2Cannon = Image.FromFile(Model.CurrentSession.Player2CannonImage);
                
            }
            catch
            {
                // sprites not found — will fall back to drawing rectangles
            }
            
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        // ─────────────────────────────────────────────────────────────
        //  START GAME
        // ─────────────────────────────────────────────────────────────

        private void StartGame()
        {
            _engine = new GameEngine(this.ClientSize.Width, this.ClientSize.Height);

            // assign sprites to tanks
            var player = new HeavyTank("Player 1", 0);
            player.BodySprite = _Tank1Body;
            player.BarrelSprite = _Tank1Cannon;

            var enemy = new LightTank("Player 2", 1);
            enemy.BodySprite = _Tank2Body;
            enemy.BarrelSprite = _Tank2Cannon;

            var ai = new AiTank(difficulty: 2);
            ai.BodySprite = _aiBody;
            ai.BarrelSprite = _aiCannon;

            var tanks = new List<Tank> { player, enemy };
            // add ai if you want 3 players: tanks.Add(ai);

            // subscribe to engine events
            _engine.OnTick += () => this.Invalidate();
            _engine.OnTurnChanged += (tank) => UpdateHUD(tank);
            _engine.OnTankDied += (tank) => ShowMessage($"{tank.PlayerName} destroyed!");
            _engine.OnGameOver += (tank) => ShowGameOver(tank);

            _engine.StartGame(tanks);
        }

        // ─────────────────────────────────────────────────────────────
        //  PAINT — everything drawn here
        // ─────────────────────────────────────────────────────────────

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. background sky
            DrawBackground(g);

            // 2. terrain
            DrawTerrain(g);

            // 3. tanks
            foreach (var tank in _engine.Tanks)
                if (tank.IsAlive)
                    DrawTank(g, tank);

            // 4. projectile
            if (_engine.ActiveProjectile != null && _engine.ActiveProjectile.IsActive)
                DrawProjectile(g, _engine.ActiveProjectile);

            // 5. HUD
            DrawHUD(g);

            if (_gameOver)
            {
                // dark overlay
                using (var brush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                    g.FillRectangle(brush, 0, 0, ClientSize.Width, ClientSize.Height);

                // winner text
                using (var font = new Font("Impact", 60, FontStyle.Bold))
                {
                    string text = $"{_winnerName} WINS!";
                    SizeF size = g.MeasureString(text, font);
                    g.DrawString(text, font, Brushes.OrangeRed,
                        ClientSize.Width / 2f - size.Width / 2f,
                        ClientSize.Height / 2f - size.Height / 2f);
                }

                // restart hint
                using (var font = new Font("Arial", 20))
                {
                    string hint = "Press R to play again  |  ESC to exit";
                    SizeF size = g.MeasureString(hint, font);
                    g.DrawString(hint, font, Brushes.White,
                        ClientSize.Width / 2f - size.Width / 2f,
                        ClientSize.Height / 2f + 60);
                }
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  DRAW BACKGROUND
        // ─────────────────────────────────────────────────────────────

        private void DrawBackground(Graphics g)
        {
            using (var brush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, this.ClientSize.Height),
                Color.FromArgb(30, 60, 120),   // dark blue top
                Color.FromArgb(10, 20, 50)))    // darker bottom
            {
                g.FillRectangle(brush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  DRAW TERRAIN
        // ─────────────────────────────────────────────────────────────

        private void DrawTerrain(Graphics g)
        {
            if (_engine.Ground == null) return;

            int[] ground = _engine.Ground;
            int w = ground.Length;
            int h = this.ClientSize.Height;

            // build polygon
            Point[] points = new Point[w + 2];
            for (int x = 0; x < w; x++)
                points[x] = new Point(x, ground[x]);

            points[w] = new Point(w - 1, h);
            points[w + 1] = new Point(0, h);

            // fill terrain
            using (var brush = new SolidBrush(Color.FromArgb(80, 120, 40)))
                g.FillPolygon(brush, points);

            // surface outline
            Point[] surface = new Point[w];
            for (int x = 0; x < w; x++)
                surface[x] = new Point(x, ground[x]);

            using (var pen = new Pen(Color.FromArgb(120, 180, 60), 2))
                g.DrawLines(pen, surface);
        }

        // ─────────────────────────────────────────────────────────────
        //  DRAW TANK
        // ─────────────────────────────────────────────────────────────

        private void DrawTank(Graphics g, Tank tank)
        {
            int cx = (int)tank.X;
            int cy = (int)tank.Y;
            int w = tank.Width;
            int h = tank.Height;

            // center point of tank
            float centerX = cx + w / 2f;
            float centerY = cy + h / 2f;

            var state = g.Save();

            // rotate around tank center
            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(tank.TerrainAngle);

            if (tank.BodySprite != null)
                g.DrawImage(tank.BodySprite, -w / 2, -h / 2, w, h);
            else
            {
                Color bodyColor = tank.TeamId == 0 ? Color.Green : Color.Blue;
                using (var brush = new SolidBrush(bodyColor))
                    g.FillRectangle(brush, -w / 2, -h / 2, w, h);
            }

            g.Restore(state);

            DrawBarrel(g, tank);
            DrawHealthBar(g, tank);

            // name above tank
            SizeF nameSize = g.MeasureString(tank.PlayerName, _nameFont);
            g.DrawString(tank.PlayerName, _nameFont, Brushes.White,
                centerX - nameSize.Width / 2f, cy - 35);
        }

        private void DrawBarrel(Graphics g, Tank tank)
        {
            float centerX = tank.X + tank.Width / 2f;
            float centerY = tank.Y + tank.Height / 3f;

            int barrelW = tank.Width;        // was tank.Width / 2, now full width
            int barrelH = tank.Height / 3;   // was tank.Height / 5, now bigger

            var state = g.Save();

            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(-tank.BarrelAngle);

            if (tank.BarrelSprite != null)
                g.DrawImage(tank.BarrelSprite, 0, -barrelH / 2, barrelW, barrelH);
            else
            {
                using (var brush = new SolidBrush(Color.DarkGray))
                    g.FillRectangle(brush, 0, -barrelH / 2, barrelW, barrelH);
            }

            g.Restore(state);
        }

        private void DrawHealthBar(Graphics g, Tank tank)
        {
            int barW = tank.Width;
            int barH = 6;
            int x = (int)tank.X;
            int y = (int)tank.Y - 16;

            // background
            g.FillRectangle(Brushes.DarkRed, x, y, barW, barH);

            // health fill
            float pct = (float)tank.Health / tank.MaxHealth;
            Color fillColor = pct > 0.5f ? Color.LimeGreen :
                              pct > 0.25f ? Color.Orange : Color.Red;

            using (var brush = new SolidBrush(fillColor))
                g.FillRectangle(brush, x, y, (int)(barW * pct), barH);

            // border
            g.DrawRectangle(Pens.Black, x, y, barW, barH);
        }

        // ─────────────────────────────────────────────────────────────
        //  DRAW PROJECTILE
        // ─────────────────────────────────────────────────────────────

        private void DrawProjectile(Graphics g, Projectile p)
        {
            int size = 8;
            using (var brush = new SolidBrush(Color.OrangeRed))
                g.FillEllipse(brush, p.X - size / 2, p.Y - size / 2, size, size);

            // glow effect
            using (var pen = new Pen(Color.Yellow, 1))
                g.DrawEllipse(pen, p.X - size, p.Y - size, size * 2, size * 2);
        }

        // ─────────────────────────────────────────────────────────────
        //  HUD
        // ─────────────────────────────────────────────────────────────

        private void DrawHUD(Graphics g)
        {
            if (_engine.Tanks == null || _engine.Tanks.Count == 0) return;

            Tank current = _engine.CurrentTank;

            // top left — current turn info
            string turnText = $"Turn: {current.PlayerName}";
            g.DrawString(turnText, _hudFont, Brushes.White, 10, 10);

            string angleText = $"Angle: {current.BarrelAngle:F0}°";
            g.DrawString(angleText, _hudFont, Brushes.Yellow, 10, 30);

            string powerText = $"Power: {current.FirePower:F0}";
            g.DrawString(powerText, _hudFont, Brushes.OrangeRed, 10, 50);

            string fuelText = $"Fuel: {current.Fuel}";
            g.DrawString(fuelText, _hudFont, Brushes.LightBlue, 10, 70);

            string weaponText = $"Weapon: {current.CurrentWeapon?.Name ?? "None"}";
            g.DrawString(weaponText, _hudFont, Brushes.LightGreen, 10, 90);

            // wind indicator top center
            string windText = $"Wind: {(_engine.WindStrength >= 0 ? "→" : "←")} " +
                              $"{Math.Abs(_engine.WindStrength * 100):F0}%";
            SizeF ws = g.MeasureString(windText, _hudFont);
            g.DrawString(windText, _hudFont, Brushes.LightCyan,
                this.ClientSize.Width / 2f - ws.Width / 2f, 10);

            // bottom controls hint
            string controls = "A/D: Move   W/S: Aim   Q/E: Power   Space: Fire   1/2: Weapon";
            SizeF cs = g.MeasureString(controls, _hudFont);
            g.DrawString(controls, _hudFont, Brushes.Gray,
                this.ClientSize.Width / 2f - cs.Width / 2f,
                this.ClientSize.Height - 30);
        }

        private void UpdateHUD(Tank tank)
        {
            // Invalidate is enough — HUD redraws every frame via OnPaint
        }

        // ─────────────────────────────────────────────────────────────
        //  KEYBOARD INPUT
        // ─────────────────────────────────────────────────────────────

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: _engine.MoveCurrentTank(-1f); break;  // move left
                case Keys.D: _engine.MoveCurrentTank(1f); break;  // move right
                case Keys.W: _engine.AdjustBarrelAngle(2f); break;  // aim up
                case Keys.S: _engine.AdjustBarrelAngle(-2f); break; // aim down
                case Keys.Q: _engine.AdjustFirePower(-2f); break;   // power down
                case Keys.E: _engine.AdjustFirePower(2f); break;   // power up
                case Keys.Space: _engine.PlayerFire(); break;   // fire
                case Keys.D1: _engine.SwitchWeapon(0); break;   // cannon
                case Keys.D2: _engine.SwitchWeapon(1); break;   // cluster
                case Keys.R:
                    if (_gameOver)
                    {
                        _gameOver = false;
                        _winnerName = "";
                        StartGame();
                    }
                    break;
                case Keys.Escape:
                    _engine.StopGame();
                    this.Close();
                    break;
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  MESSAGES
        // ─────────────────────────────────────────────────────────────

        private void ShowMessage(string msg)
        {
            // runs on UI thread
            if (this.InvokeRequired)
                this.Invoke(new Action(() => ShowMessage(msg)));
            else
                MessageBox.Show(msg);
        }

        private void ShowGameOver(Tank winner)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowGameOver(winner)));
                return;
            }

            _engine.StopGame();

            // draw winner screen directly on form
            _gameOver = true;
            _winnerName = winner.PlayerName;
            this.Invalidate();
        }

        // ─────────────────────────────────────────────────────────────
        //  CLEANUP
        // ─────────────────────────────────────────────────────────────

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _engine?.StopGame();
            base.OnFormClosing(e);
        }
    }
}