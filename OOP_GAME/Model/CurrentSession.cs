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
        public static string Player1BodyImage { get; set; }
        public static string Player1CannonImage { get; set; }
        public static string Player2BodyImage { get; set; }
        public static string Player2CannonImage { get; set; }

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
    }
}
