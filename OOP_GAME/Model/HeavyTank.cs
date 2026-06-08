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
        public HeavyTank(string playerName, int teamId) : base(playerName, teamId)
        {
            MaxHealth = 200;  // same as HeavyTank
            Health = 200;
            MaxFuel = 60;     // same
            Fuel = 60;
            Width = 80;       // same
            Height = 50;
        }

        public override void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }

        public override Projectile Fire()
        {
            return CurrentWeapon.CreateProjectile(X, Y, BarrelAngle, FirePower);
        }
    }
}
