
using OOP_GAME.Model;
using System;
using System.Collections.Generic;

public class PhysicsEngine
{
    private const float Gravity = 0.4f;
    private const float WindEffect = 0.02f;

    public float WindStrength { get; set; } = 0f;
    public void UpdateProjectile(Projectile p)
    {
        p.VelocityY += Gravity; 
        p.VelocityX += WindStrength; 
        p.X += p.VelocityX;
        p.Y += p.VelocityY;
    }

    public void UpdateTank(Tank tank, int[] ground)
    {
        // clamp to panel edges
        if (tank.X < 0) tank.X = 0;
        if (tank.X > ground.Length - tank.Width - 1)
            tank.X = ground.Length - tank.Width - 1;

        // use CENTER of tank for ground lookup, not left edge
        int centerX = (int)(tank.X + tank.Width / 2f);
        centerX = Math.Max(1, Math.Min(centerX, ground.Length - 2));

        // snap to ground
        tank.Y = ground[centerX] - tank.Height;

        // angle based on center
        float dy = ground[centerX + 1] - ground[centerX - 1];
        tank.TerrainAngle = (float)(Math.Atan2(dy, 2f) * 180.0 / Math.PI);
    }
    public ImpactResult CheckImpact(Projectile p, int[] ground, List<Tank> tanks)
    {
        int px = (int)p.X;
        int py = (int)p.Y;

        if (px >= 0 && px < ground.Length && py >= ground[px])
            return new ImpactResult { HitGround = true, X = px, Y = py };
        foreach (var tank in tanks)
        {
            if (!tank.IsAlive) continue;
            if (px >= tank.X && px <= tank.X + tank.Width &&
                py >= tank.Y && py <= tank.Y + tank.Height)
                return new ImpactResult { HitTank = tank, X = px, Y = py };
        }

        if (px < 0 || px >= ground.Length || py > ground.Length)
            return new ImpactResult { OutOfBounds = true };

        return null;
    }


    public void ApplySplashDamage(List<Tank> tanks, int impactX, int impactY,
                               int blastRadius, int damage)
    {
        foreach (var tank in tanks)
        {
            // use CENTER of tank not top-left corner
            float tankCenterX = tank.X + tank.Width / 2f;
            float tankCenterY = tank.Y + tank.Height / 2f;

            float dist = (float)Math.Sqrt(
                Math.Pow(tankCenterX - impactX, 2) +
                Math.Pow(tankCenterY - impactY, 2));

            if (dist <= blastRadius)
            {
                float falloff = 1f - (dist / blastRadius);
                int dmg = (int)(damage * falloff);
                tank.TakeDamage(dmg);
            }
        }
    }
}
public class ImpactResult
{
    public bool HitGround { get; set; }
    public bool OutOfBounds { get; set; }
    public Tank HitTank { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}