using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/Tank.cs
    public abstract class Tank
    {
        // position and movement
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        // stats
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Fuel { get; set; }
        public int MaxFuel { get; set; }

        // aiming
        public float BarrelAngle { get; set; }   // degrees
        public float FirePower { get; set; }      // 0-100
        public float TerrainAngle { get; set; }   // for rotating body to match slope

        // identity
        public string PlayerName { get; set; }
        public int TeamId { get; set; }
        public bool IsAlive => Health > 0;
        public bool IsAI { get; set; }

        // sprite
        public Image BodySprite { get; set; }
        public Image BarrelSprite { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // current weapon
        public Weapon CurrentWeapon { get; set; }
        public List<Weapon> WeaponInventory { get; set; } = new List<Weapon>();

        protected Tank(string playerName, int teamId)
        {
            PlayerName = playerName;
            TeamId = teamId;
            BarrelAngle = 45f;
            FirePower = 50f;
            Width = 64;
            Height = 40;
        }

        public abstract void TakeDamage(int amount);
        public abstract Projectile Fire();
    }
}
