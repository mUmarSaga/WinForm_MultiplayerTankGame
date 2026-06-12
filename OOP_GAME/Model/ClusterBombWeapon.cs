using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    
    public class ClusterBombWeapon : Weapon
    {
        public ClusterBombWeapon() : base("Cluster Bomb", ammo: 3, damage: 30, blastRadius: 20) { }

        public override Projectile CreateProjectile(float x, float y, float angle, float power)
        {
            return new ClusterBomb(x, y, angle, power);
        }
    }
}
