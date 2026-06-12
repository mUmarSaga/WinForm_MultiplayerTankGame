using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    
    public class LightTank : Tank
    {
        public LightTank(string playerName, int teamId) : base(playerName, teamId)
        {
            MaxHealth = 200; 
            Health = 200;
            MaxFuel = 60;    
            Fuel = 60;
            Width = 80;      
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
