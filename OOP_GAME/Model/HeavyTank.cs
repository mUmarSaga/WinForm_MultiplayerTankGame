using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/HeavyTank.cs
    public class HeavyTank : Tank
    {
        public int ArmorRating { get; set; }   // reduces incoming damage

        public HeavyTank(string playerName, int teamId) : base(playerName, teamId)
        {
            MaxHealth = 200;
            Health = 200;
            MaxFuel = 60;        // moves less
            Fuel = 60;
            ArmorRating = 20;    // 20% damage reduction
            Width = 80;
            Height = 50;         // bigger sprite
        }

        public override void TakeDamage(int amount)
        {
            int reduced = amount - (amount * ArmorRating / 100);
            Health = Math.Max(0, Health - reduced);
        }

        public override Projectile Fire()
        {
            return CurrentWeapon.CreateProjectile(X, Y, BarrelAngle, FirePower);
        }
    }
}
