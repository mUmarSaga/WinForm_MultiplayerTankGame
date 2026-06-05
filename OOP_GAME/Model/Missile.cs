using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.Model
{
    // Models/Missile.cs
    public class Missile : Projectile
    {
        public Tank Target { get; set; }
        public float TrackingStrength { get; set; } = 0.05f;

        public Missile(float x, float y, float angle, float power, Tank target)
            : base(x, y, angle, power, damage: 70, blastRadius: 50)
        {
            Target = target;
        }

        public override void Update()
        {
            // slight course correction toward target, done in PhysicsEngine
        }

        public override void OnImpact(int[] ground, List<Tank> tanks)
        {
            IsActive = false;
        }
    }
}
