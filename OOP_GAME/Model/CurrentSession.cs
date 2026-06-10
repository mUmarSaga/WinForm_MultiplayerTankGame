using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/CurrentSession.cs
    public class CurrentSession
    {
        public Tank PlayerTank { get; set; }
        public Tank EnemyTank { get; set; }
        public int[] Terrain { get; set; }
        public int RoundNumber { get; set; } = 1;
        public bool IsPlayerTurn { get; set; } = true;
        public static string Player1BodyImage = "assets/heavyTank1Body.png";
        public static string Player1CannonImage = "assets/heavyTank1Cannon.png";
        public static string Player2BodyImage = "assets/heavyTank2Body.png";
        public static string Player2CannonImage = "assets/heavyTank2Cannon.png";

        // ─── networking ──────────────────────────────────────────────
        public bool IsHost { get; set; }
        public string LocalPlayerName { get; set; }
        public string RemotePlayerName { get; set; }
        public int TerrainSeed { get; set; }
        public float InitialWind { get; set; }

        // ─── appearance (loaded from Settings, sent over network) ───
        public int LocalBodyIndex { get; set; }
        public int LocalCannonIndex { get; set; }
        public int RemoteBodyIndex { get; set; }
        public int RemoteCannonIndex { get; set; }

        // sprite path arrays for lookup by index
        public static readonly string[] BodyImages =
        {
            "assets/heavyTank1Body.png",
            "assets/heavyTank2Body.png",
            "assets/heavyTank3Body.png"
        };

        public static readonly string[] CannonImages =
        {
            "assets/heavyTank1Cannon.png",
            "assets/heavyTank2Cannon.png",
            "assets/heavyTank3Cannon.png"
        };

        // singleton — one session at a time
        private static CurrentSession _instance;
        public static CurrentSession Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CurrentSession();
                return _instance;
            }
        }

        public static void StartNew(Tank player, Tank enemy)
        {
            _instance = new CurrentSession
            {
                PlayerTank = player,
                EnemyTank = enemy
            };
        }

        /// <summary>
        /// Load the saved appearance indices from Properties.Settings
        /// </summary>
        public void LoadAppearanceFromSettings()
        {
            LocalBodyIndex = Properties.Settings.Default.BodySpriteIndex;
            LocalCannonIndex = Properties.Settings.Default.CannonSpriteIndex;
        }
    }
}
