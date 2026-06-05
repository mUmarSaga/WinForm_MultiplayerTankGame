using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/Weapon.cs
    public abstract class Weapon
    {
        public string Name { get; set; }
        public int Ammo { get; set; }          // -1 = unlimited
        public int Damage { get; set; }
        public int BlastRadius { get; set; }
        public Image Icon { get; set; }

        protected Weapon(string name, int ammo, int damage, int blastRadius)
        {
            Name = name;
            Ammo = ammo;
            Damage = damage;
            BlastRadius = blastRadius;
        }

        public abstract Projectile CreateProjectile(float x, float y, float angle, float power);

        public bool CanFire() => Ammo == -1 || Ammo > 0;

        public void UseAmmo()
        {
            if (Ammo > 0) Ammo--;
        }
    }
}
