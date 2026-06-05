using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    internal class ClusterBomb : Projectile
    {
        public int NumClusters { get; set; } = 5;
        public bool HasSplit { get; set; } = false;
        public float SplitAltitude { get; set; }
        public ClusterBomb(float x, float y, float angle, float power)
            : base(x, y, angle, power, damage: 30, blastRadius: 20)
        {
            NumClusters = 5;

        }
        public override void OnImpact(int[] ground, List<Tank> tanks)
        {
            IsActive = false;
            throw new NotImplementedException();
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
