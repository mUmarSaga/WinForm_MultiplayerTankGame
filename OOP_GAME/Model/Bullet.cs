using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    internal class Bullet : Projectile
    {
        public Bullet(float x, float y, float angle, float power)
            : base(x, y, angle, power, damage: 100, blastRadius: 50)
        {
            
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void OnImpact(int[] ground, List<Tank> tanks)
        {
            IsActive = false;
            throw new NotImplementedException();
        }
    }
}
