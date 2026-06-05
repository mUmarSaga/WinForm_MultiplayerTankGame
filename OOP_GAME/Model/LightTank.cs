using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/LightTank.cs
    public class LightTank : Tank
    {
        public float SpeedBonus { get; set; }  // moves faster

        public LightTank(string playerName, int teamId) : base(playerName, teamId)
        {
            MaxHealth = 120;
            Health = 120;
            MaxFuel = 120;       // moves more
            Fuel = 120;
            SpeedBonus = 1.5f;
            Width = 52;
            Height = 34;         // smaller sprite
        }

        public override void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount); // no armor, full damage
        }

        public override Projectile Fire()
        {
            return CurrentWeapon.CreateProjectile(X, Y, BarrelAngle, FirePower);
        }
    }
}
