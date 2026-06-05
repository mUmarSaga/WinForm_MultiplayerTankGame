using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/Cannon.cs
    public class Cannon : Weapon
    {
        public Cannon() : base("Cannon", ammo: -1, damage: 20, blastRadius: 100) { }

        public override Projectile CreateProjectile(float x, float y, float angle, float power)
        {
            return new Bullet(x, y, angle, power);
        }
    }
}
