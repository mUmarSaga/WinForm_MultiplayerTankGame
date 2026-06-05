using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/RocketLauncher.cs
    public class RocketLauncher : Weapon
    {
        private Tank _target;

        public RocketLauncher(Tank target) : base("Rocket", ammo: 3, damage: 70, blastRadius: 50)
        {
            _target = target;
        }

        public override Projectile CreateProjectile(float x, float y, float angle, float power)
        {
            return new Missile(x, y, angle, power, _target);
        }
    }
}
