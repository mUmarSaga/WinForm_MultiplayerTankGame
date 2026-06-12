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
        
        private GameEngine _engine;

        
        private Image _Tank1Body;
        private Image _Tank2Body;
        private Image _Tank1Cannon;
        private Image _Tank2Cannon;
        private Image _aiBody;
        private Image _aiCannon;
        private Image _bgImage;

        private bool _gameOver = false;
        private string _winnerName = "";

        
        private int _turnFlashFrames = 0;
        private const int TurnFlashDuration = 40;

        
        private string _crateNotification = "";
        private int _crateNotifyFrames = 0;

        
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
            


            this.KeyDown += GameForm_KeyDown;

            LoadSprites();
            this.Load += GameForm_Load;

        }


        private void LoadSprites()
        {
            try
            {
                _Tank1Body = Image.FromFile("assets/heavyTank1Body.png");
                _Tank1Cannon = Image.FromFile("assets/heavyTank1Cannon.png");
                _Tank2Body = Image.FromFile("assets/heavyTank2Body.png");
                _Tank2Cannon = Image.FromFile("assets/heavyTank2Cannon.png");
                _bgImage = Image.FromFile("assets/sky.png");
            }
            catch
            {
                
            }
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        
        
        

        private void StartGame()
        {
            _engine = new GameEngine(this.ClientSize.Width, this.ClientSize.Height);

            var session = CurrentSession.Instance;
            string hostName = session.IsHost ? session.LocalPlayerName : session.RemotePlayerName;
            string guestName = session.IsHost ? session.RemotePlayerName : session.LocalPlayerName;

            
            int hostBodyIdx = session.IsHost ? session.LocalBodyIndex : session.RemoteBodyIndex;
            int hostCannonIdx = session.IsHost ? session.LocalCannonIndex : session.RemoteCannonIndex;
            int guestBodyIdx = session.IsHost ? session.RemoteBodyIndex : session.LocalBodyIndex;
            int guestCannonIdx = session.IsHost ? session.RemoteCannonIndex : session.LocalCannonIndex;

            
            Image hostBodySprite = LoadSprite(CurrentSession.BodyImages[hostBodyIdx]);
            Image hostCannonSprite = LoadSprite(CurrentSession.CannonImages[hostCannonIdx]);
            Image guestBodySprite = LoadSprite(CurrentSession.BodyImages[guestBodyIdx]);
            Image guestCannonSprite = LoadSprite(CurrentSession.CannonImages[guestCannonIdx]);

            
            var hostTank = new HeavyTank(hostName, 0);
            hostTank.BodySprite = hostBodySprite;
            hostTank.BarrelSprite = hostCannonSprite;

            var guestTank = new LightTank(guestName, 1);
            guestTank.BodySprite = guestBodySprite;
            guestTank.BarrelSprite = guestCannonSprite;

            var tanks = new List<Tank> { hostTank, guestTank };

            
            _engine.OnTick += () => this.Invalidate();
            _engine.OnTurnChanged += (tank) =>
            {
                _turnFlashFrames = TurnFlashDuration;
                UpdateHUD(tank);
            };
            _engine.OnTankDied += (tank) => ShowMessage($"{tank.PlayerName} destroyed!");
            _engine.OnGameOver += (tank) => ShowGameOver(tank);
            _engine.OnCrateCollected += (crate, tank) =>
            {
                string type = crate.Type == OOP_GAME.Model.CrateType.Health ? "+30 HP" : "+1 Ammo";
                _crateNotification = $"{tank.PlayerName} picked up {type}!";
                _crateNotifyFrames = 90;
            };

            
            NetworkManager.Instance.OnMessageReceived += Network_MessageReceived;

            _engine.StartGame(tanks, session.TerrainSeed, session.InitialWind);
        }

        private Image LoadSprite(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch
            {
                return null;
            }
        }

        private void Network_MessageReceived(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => _engine.HandleNetworkMessage(msg)));
            }
            else
            {
                _engine.HandleNetworkMessage(msg);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            
            DrawBackground(g);

            
            DrawTerrain(g);

            
            DrawSupplyCrates(g);

            
            foreach (var tank in _engine.Tanks)
                if (tank.IsAlive)
                    DrawTank(g, tank);

            
            if (!_engine.IsProjectileFlying && _engine.IsLocalPlayersTurn() && !_engine.IsGameOver)
                DrawTrajectoryPreview(g);

            
            if (_engine.ActiveProjectile != null && _engine.ActiveProjectile.IsActive)
                DrawProjectile(g, _engine.ActiveProjectile);

            
            DrawHUD(g);

            
            DrawCrateNotification(g);

            if (_gameOver)
            {
            
                using (var brush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                    g.FillRectangle(brush, 0, 0, ClientSize.Width, ClientSize.Height);

                
                using (var font = new Font("Impact", 60, FontStyle.Bold))
                {
                    string text = $"{_winnerName} WINS!";
                    SizeF size = g.MeasureString(text, font);
                    g.DrawString(text, font, Brushes.OrangeRed,
                        ClientSize.Width / 2f - size.Width / 2f,
                        ClientSize.Height / 2f - size.Height / 2f);
                }

                
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


        private void DrawBackground(Graphics g)
        {
            if (_bgImage != null)
                g.DrawImage(_bgImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            else
            {
                using (var brush = new LinearGradientBrush(
                    new Point(0, 0), new Point(0, this.ClientSize.Height),
                    Color.FromArgb(30, 60, 120), Color.FromArgb(10, 20, 50)))
                    g.FillRectangle(brush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
        }
        private void DrawTerrain(Graphics g)
        {
            if (_engine.Ground == null) return;

            int[] ground = _engine.Ground;
            int w = ground.Length;
            int h = this.ClientSize.Height;

            
            Point[] points = new Point[w + 2];
            for (int x = 0; x < w; x++)
                points[x] = new Point(x, ground[x]);

            points[w] = new Point(w - 1, h);
            points[w + 1] = new Point(0, h);

            
            using (var brush = new SolidBrush(Color.FromArgb(80, 120, 40)))
                g.FillPolygon(brush, points);

            
            Point[] surface = new Point[w];
            for (int x = 0; x < w; x++)
                surface[x] = new Point(x, ground[x]);

            using (var pen = new Pen(Color.FromArgb(120, 180, 60), 2))
                g.DrawLines(pen, surface);
        }


        private void DrawTank(Graphics g, Tank tank)
        {
            int cx = (int)tank.X;
            int cy = (int)tank.Y;
            int w = tank.Width;
            int h = tank.Height;

            
            float centerX = cx + w / 2f;
            float centerY = cy + h / 2f;

            var state = g.Save();

            
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

            
            SizeF nameSize = g.MeasureString(tank.PlayerName, _nameFont);
            g.DrawString(tank.PlayerName, _nameFont, Brushes.White,
                centerX - nameSize.Width / 2f, cy - 35);
        }

        private void DrawBarrel(Graphics g, Tank tank)
        {
            float centerX = tank.X + tank.Width / 2f;
            float centerY = tank.Y + tank.Height / 3f;

            int barrelW = tank.Width;        
            int barrelH = tank.Height / 3;   

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

            
            g.FillRectangle(Brushes.DarkRed, x, y, barW, barH);

            
            float pct = (float)tank.Health / tank.MaxHealth;
            Color fillColor = pct > 0.5f ? Color.LimeGreen :
                              pct > 0.25f ? Color.Orange : Color.Red;

            using (var brush = new SolidBrush(fillColor))
                g.FillRectangle(brush, x, y, (int)(barW * pct), barH);

            
            g.DrawRectangle(Pens.Black, x, y, barW, barH);
        }

        
        
        

        private void DrawProjectile(Graphics g, Projectile p)
        {
            int size = 8;
            using (var brush = new SolidBrush(Color.OrangeRed))
                g.FillEllipse(brush, p.X - size / 2, p.Y - size / 2, size, size);

            using (var pen = new Pen(Color.Yellow, 1))
                g.DrawEllipse(pen, p.X - size, p.Y - size, size * 2, size * 2);
        }


        private void DrawHUD(Graphics g)
        {
            if (_engine.Tanks == null || _engine.Tanks.Count == 0) return;

            Tank current = _engine.CurrentTank;

            
            DrawTurnBanner(g, current);

            
            
            using (var bgBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
            {
                var bgRect = new RectangleF(5, 50, 200, 80);
                g.FillRectangle(bgBrush, bgRect);
            }

            string angleText = $"Angle: {current.BarrelAngle:F0}°";
            g.DrawString(angleText, _hudFont, Brushes.Yellow, 12, 55);

            string powerText = $"Power: {current.FirePower:F0}";
            g.DrawString(powerText, _hudFont, Brushes.OrangeRed, 12, 75);

            string fuelText = $"Fuel: {current.Fuel}";
            g.DrawString(fuelText, _hudFont, Brushes.LightBlue, 12, 95);

            
            DrawWeaponPanel(g, current);

            
            string windText = $"Wind: {(_engine.WindStrength >= 0 ? "→" : "←")} " +
                              $"{Math.Abs(_engine.WindStrength * 100):F0}%";
            SizeF ws = g.MeasureString(windText, _hudFont);
            g.DrawString(windText, _hudFont, Brushes.LightCyan,
                this.ClientSize.Width / 2f - ws.Width / 2f, 45);

            
            string controls = "A/D: Move   W/S: Aim   Q/E: Power   Space: Fire   1/2: Weapon";
            SizeF cs = g.MeasureString(controls, _hudFont);
            g.DrawString(controls, _hudFont, Brushes.Gray,
                this.ClientSize.Width / 2f - cs.Width / 2f,
                this.ClientSize.Height - 30);
        }

        
        
        

        private void DrawTurnBanner(Graphics g, Tank current)
        {
            string turnText = $"► {current.PlayerName}'s Turn";
            using (var bannerFont = new Font("Arial", 16, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(turnText, bannerFont);
                float bannerW = textSize.Width + 40;
                float bannerH = 34;
                float bannerX = ClientSize.Width / 2f - bannerW / 2f;
                float bannerY = 5;

                
                Color teamColor = current.TeamId == 0
                    ? Color.FromArgb(60, 180, 75)  
                    : Color.FromArgb(70, 130, 230);

                
                int bgAlpha = 160;
                if (_turnFlashFrames > 0)
                {
                    float t = (float)_turnFlashFrames / TurnFlashDuration;
                    bgAlpha = (int)(160 + 80 * t);
                    _turnFlashFrames--;
                }
                bgAlpha = Math.Min(bgAlpha, 240);

                Color bgColor = Color.FromArgb(bgAlpha, teamColor.R, teamColor.G, teamColor.B);

                
                RectangleF bannerRect = new RectangleF(bannerX, bannerY, bannerW, bannerH);
                using (var bgBrush = new SolidBrush(bgColor))
                    g.FillRectangle(bgBrush, bannerRect);

                using (var borderPen = new Pen(Color.FromArgb(200, 255, 255, 255), 1.5f))
                    g.DrawRectangle(borderPen, bannerX, bannerY, bannerW, bannerH);

                
                g.DrawString(turnText, bannerFont, Brushes.White,
                    bannerX + 20, bannerY + (bannerH - textSize.Height) / 2f);
            }
        }

        
        private void DrawWeaponPanel(Graphics g, Tank current)
        {
            int panelW = 200;
            int lineH = 22;
            int panelH = 10 + current.WeaponInventory.Count * lineH + 5;
            int panelX = ClientSize.Width - panelW - 10;
            int panelY = 50;

            
            using (var bgBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
                g.FillRectangle(bgBrush, panelX, panelY, panelW, panelH);

            
            using (var titleFont = new Font("Arial", 10, FontStyle.Bold))
                g.DrawString("WEAPONS", titleFont, Brushes.White, panelX + 8, panelY + 4);

            
            using (var weaponFont = new Font("Arial", 10))
            {
                for (int i = 0; i < current.WeaponInventory.Count; i++)
                {
                    var w = current.WeaponInventory[i];
                    bool isSelected = (w == current.CurrentWeapon);
                    int yPos = panelY + 22 + i * lineH;

                    
                    if (isSelected)
                    {
                        using (var hlBrush = new SolidBrush(Color.FromArgb(80, 255, 255, 100)))
                            g.FillRectangle(hlBrush, panelX + 4, yPos - 1, panelW - 8, lineH);
                    }

                    string ammoStr = w.Ammo == -1 ? "∞" : w.Ammo.ToString();
                    string keyHint = $"[{i + 1}]";
                    string line = $"{keyHint} {w.Name}  x{ammoStr}";

                    Brush textBrush = isSelected ? Brushes.Yellow : Brushes.LightGray;
                    
                    if (w.Ammo == 0)
                        textBrush = Brushes.DarkGray;

                    g.DrawString(line, weaponFont, textBrush, panelX + 8, yPos);
                }
            }
        }



        private void DrawTrajectoryPreview(Graphics g)
        {
            var points = _engine.GetTrajectoryPreview(15);
            for (int i = 0; i < points.Count; i++)
            {
                float size = 3f;

                int alpha = (int)(150 * (1f - (float)i / points.Count));
                using (var fadeBrush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)))
                    g.FillEllipse(fadeBrush, points[i].X - size / 2, points[i].Y - size / 2, size, size);
            }
        }



        private void DrawSupplyCrates(Graphics g)
        {
            foreach (var crate in _engine.ActiveCrates)
            {
                if (!crate.IsActive) continue;

                float cx = crate.X;
                float cy = crate.Y;
                int cw = crate.Width;
                int ch = crate.Height;


                if (!crate.HasLanded)
                {
                    float parachuteW = cw * 2.2f;
                    float parachuteH = cw * 1.2f;
                    float pcx = cx + cw / 2f + crate.SwayAngle * 0.3f;
                    float pcy = cy - parachuteH + 2;

                    
                    Color domeColor = crate.Type == CrateType.Health
                        ? Color.FromArgb(180, 220, 80, 80) : Color.FromArgb(180, 220, 200, 50);
                    using (var domeBrush = new SolidBrush(domeColor))
                        g.FillEllipse(domeBrush, pcx - parachuteW / 2, pcy, parachuteW, parachuteH);

                    
                    using (var stringPen = new Pen(Color.FromArgb(180, 200, 200, 200), 1))
                    {
                        float crateCenter = cx + cw / 2f;
                        g.DrawLine(stringPen, pcx - parachuteW / 3f, pcy + parachuteH * 0.7f, crateCenter, cy);
                        g.DrawLine(stringPen, pcx, pcy + parachuteH * 0.5f, crateCenter, cy);
                        g.DrawLine(stringPen, pcx + parachuteW / 3f, pcy + parachuteH * 0.7f, crateCenter, cy);
                    }
                }

               
                if (crate.Type == CrateType.Health)
                {
               
                    using (var boxBrush = new SolidBrush(Color.FromArgb(220, 40, 160, 60)))
                        g.FillRectangle(boxBrush, cx, cy, cw, ch);
                    using (var crossPen = new Pen(Color.White, 2))
                    {
                        g.DrawLine(crossPen, cx + cw / 2, cy + 4, cx + cw / 2, cy + ch - 4);
                        g.DrawLine(crossPen, cx + 4, cy + ch / 2, cx + cw - 4, cy + ch / 2);
                    }
                }
                else
                {
                    
                    using (var boxBrush = new SolidBrush(Color.FromArgb(220, 200, 160, 30)))
                        g.FillRectangle(boxBrush, cx, cy, cw, ch);
                    using (var ammoFont = new Font("Arial", 10, FontStyle.Bold))
                        g.DrawString("A", ammoFont, Brushes.White, cx + 5, cy + 3);
                }

                
                using (var borderPen = new Pen(Color.FromArgb(180, 60, 40, 20), 1.5f))
                    g.DrawRectangle(borderPen, cx, cy, cw, ch);
            }
        }

        
        
        

        private void DrawCrateNotification(Graphics g)
        {
            if (_crateNotifyFrames <= 0) return;

            float alpha = Math.Min(255, _crateNotifyFrames * 6);
            using (var font = new Font("Arial", 14, FontStyle.Bold))
            {
                SizeF size = g.MeasureString(_crateNotification, font);
                float x = ClientSize.Width / 2f - size.Width / 2f;
                float y = ClientSize.Height / 2f - 100;

                using (var bgBrush = new SolidBrush(Color.FromArgb((int)(alpha * 0.5), 0, 0, 0)))
                    g.FillRectangle(bgBrush, x - 10, y - 4, size.Width + 20, size.Height + 8);

                using (var textBrush = new SolidBrush(Color.FromArgb((int)alpha, 255, 230, 100)))
                    g.DrawString(_crateNotification, font, textBrush, x, y);
            }
            _crateNotifyFrames--;
        }

        private void UpdateHUD(Tank tank)
        {

        }



        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: _engine.MoveCurrentTank(-1f); break;  
                case Keys.D: _engine.MoveCurrentTank(1f); break;  
                case Keys.W: _engine.AdjustBarrelAngle(2f); break;
                case Keys.S: _engine.AdjustBarrelAngle(-2f); break;
                case Keys.Q: _engine.AdjustFirePower(-2f); break;  
                case Keys.E: _engine.AdjustFirePower(2f); break;   
                case Keys.Space: _engine.PlayerFire(); break;   
                case Keys.D1: _engine.SwitchWeapon(0); break;   
                case Keys.D2: _engine.SwitchWeapon(1); break;   
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


        private void ShowMessage(string msg)
        {
            
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

            
            _gameOver = true;
            _winnerName = winner.PlayerName;
            this.Invalidate();
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _engine?.StopGame();
            NetworkManager.Instance.OnMessageReceived -= Network_MessageReceived;
            NetworkManager.Reset();

            if (this.Owner != null)
                this.Owner.Show(); 

            base.OnFormClosing(e);
              
        }
    }
}