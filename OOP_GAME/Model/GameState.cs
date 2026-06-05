using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{

    public class GameState
    {
        public List<Tank> Tanks { get; set; }
        public int[] Terrain { get; set; }
        public int CurrentTurnIndex { get; set; }
        public int RoundNumber { get; set; }
        public DateTime SavedAt { get; set; }
    }
}
