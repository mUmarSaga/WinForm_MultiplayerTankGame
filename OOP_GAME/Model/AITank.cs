using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/AiTank.cs
    public class AiTank : Tank
    {
        public int Difficulty { get; set; }   // 1=easy, 2=medium, 3=hard
        public float AimAccuracy { get; set; } // 0-1, how close to perfect aim

        public AiTank(int difficulty) : base("CPU", teamId: 1)
        {
            Difficulty = difficulty;
            AimAccuracy = difficulty * 0.3f; // 0.3 easy, 0.6 medium, 0.9 hard
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
