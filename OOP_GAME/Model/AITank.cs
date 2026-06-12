using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    
    public class AiTank : Tank
    {
        public int Difficulty { get; set; } 
        public float AimAccuracy { get; set; }

        public AiTank(int difficulty) : base("CPU", teamId: 1)
        {
            Difficulty = difficulty;
            AimAccuracy = difficulty * 0.3f; 
            MaxHealth = 150;
            Health = 150;
            MaxFuel = 80;
            Fuel = 80;
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
