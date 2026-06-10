using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.BL
{
    // BL/TerrainManager.cs
    public class TerrainManager
    {
        public int[] Ground { get; private set; }
        private int _panelWidth;
        private int _panelHeight;

        public TerrainManager(int panelWidth, int panelHeight)
        {
            _panelWidth = panelWidth;
            _panelHeight = panelHeight;
        }

        // ── parameterless version — random seed ─────────────────────
        public void GenerateTerrain()
        {
            GenerateTerrain(new Random().Next());
        }

        // ── seeded version — deterministic for multiplayer ──────────
        public void GenerateTerrain(int seed)
        {
            Ground = new int[_panelWidth];
            var rng = new Random(seed);
            int baseline = (int)(_panelHeight * 0.75); // lower baseline (more ground visible)
            int amplitude = 25;                          // was 40, less height on hills
            double freq = 0.02 + rng.NextDouble() * 0.02; // was 0.5+, much less frequent waves

            for (int x = 0; x < _panelWidth; x++)
            {
                Ground[x] = baseline
           + (int)(amplitude * Math.Sin(x * freq))
           + (int)(amplitude * 0.5 * Math.Sin(x * freq * 2.3 + 1.5))
           + (int)(amplitude * 0.3 * Math.Sin(x * freq * 0.7 + 3.0));
            }
        }

        // ── new: flat spots so tanks don't spawn on steep slopes ───
        public void FlattenSpawnPoints(List<int> spawnXPositions, int flatWidth = 60)
        {
            foreach (int cx in spawnXPositions)
            {
                int avgY = Ground[cx]; // use center point as target height
                for (int x = cx - flatWidth / 2; x <= cx + flatWidth / 2; x++)
                {
                    if (x < 0 || x >= _panelWidth) continue;
                    Ground[x] = avgY;
                }
            }
        }

        // ── new: get slope angle at a point (used by PhysicsEngine) 
        public float GetSlopeAngle(int x)
        {
            if (x <= 0 || x >= Ground.Length - 1) return 0f;
            float dy = Ground[x + 1] - Ground[x - 1];
            float dx = 2f;
            return (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
        }

        // ── new: check if a point is below ground ──────────────────
        public bool IsBelowGround(float x, float y)
        {
            int ix = (int)Math.Max(0, Math.Min(x, Ground.Length - 1));
            return y >= Ground[ix];
        }

        // ── already in PhysicsEngine but belongs here ───────────────
        public void CreateCrater(int centerX, int radius)
        {
            for (int x = centerX - radius; x <= centerX + radius; x++)
            {
                if (x < 0 || x >= Ground.Length) continue;
                int dx = x - centerX;
                int depth = (int)Math.Sqrt(radius * radius - dx * dx);
                depth = (int)(depth * 1.5f); // multiply depth to make it deeper
                Ground[x] = Math.Min(Ground[x] + depth, _panelHeight - 10);
            }
        }
    }
}
