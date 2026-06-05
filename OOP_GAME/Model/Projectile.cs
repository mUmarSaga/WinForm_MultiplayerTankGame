using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_GAME.Model
{
    public abstract class Projectile
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float Damage { get; set; }
        public float BlastRadius { get; set; }
        public bool IsActive { get; set; } = true;
        public Image Sprite { get; set; }
        public int OwnerId { get; set; }
        protected Projectile(float x, float y,float angle,float power, float damage, float blastRadius)
        {
            X = x;
            Y = y;
            Damage = damage;
            BlastRadius = blastRadius;


            double radian = angle * Math.PI / 180;
            float speed = power * 0.15f;
            VelocityX = (float)(Math.Cos(radian) * speed);
            VelocityY = (float)(-Math.Sin(radian) * speed);

        }
        public abstract void Update();          
        public abstract void OnImpact(int[] ground, List<Tank> tanks);
    }
}
