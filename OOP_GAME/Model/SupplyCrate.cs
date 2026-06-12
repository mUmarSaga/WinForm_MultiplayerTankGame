using System;

namespace OOP_GAME.Model
{
    public enum CrateType
    {
        Health,
        Ammo
    }

    public class SupplyCrate
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; } = 24;
        public int Height { get; set; } = 24;
        public CrateType Type { get; set; }
        public bool IsActive { get; set; } = true;
        public bool HasLanded { get; set; } = false;

        
        private const float FallSpeed = 1.2f;

        
        private float _swayAngle = 0f;
        private float _swaySpeed;
        public float SwayAngle => _swayAngle;

        public SupplyCrate(float x, float y, CrateType type)
        {
            X = x;
            Y = y;
            Type = type;
            _swaySpeed = 0.03f + (float)(new Random().NextDouble() * 0.02);
        }

       =
        public bool Update(int[] ground)
        {
            if (!IsActive || HasLanded) return false;

        
            _swayAngle = (float)(Math.Sin(Y * _swaySpeed) * 8.0);

            Y += FallSpeed;

            int ix = (int)Math.Max(0, Math.Min(X, ground.Length - 1));
            if (Y + Height >= ground[ix])
            {
                Y = ground[ix] - Height;
                HasLanded = true;
                return true;
            }
            return false;
        }

       
        public bool Overlaps(Tank tank)
        {
            if (!IsActive || !HasLanded) return false;

            return tank.X + tank.Width > X &&
                   tank.X < X + Width &&
                   tank.Y + tank.Height > Y &&
                   tank.Y < Y + Height;
        }
    }
}
