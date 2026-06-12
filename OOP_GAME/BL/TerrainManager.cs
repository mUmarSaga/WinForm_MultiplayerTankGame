using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_GAME.BL
{
    
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


        public void GenerateTerrain()
        {
            GenerateTerrain(new Random().Next());
        }

        
        public void GenerateTerrain(int seed)
        {
            Ground = new int[_panelWidth];
            var rng = new Random(seed);
            int baseline = (int)(_panelHeight * 0.75); 
            int amplitude = 25;                         
            double freq = 0.02 + rng.NextDouble() * 0.02;

            for (int x = 0; x < _panelWidth; x++)
            {
                Ground[x] = baseline
           + (int)(amplitude * Math.Sin(x * freq))
           + (int)(amplitude * 0.5 * Math.Sin(x * freq * 2.3 + 1.5))
           + (int)(amplitude * 0.3 * Math.Sin(x * freq * 0.7 + 3.0));
            }
        }

        
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

        
        public float GetSlopeAngle(int x)
        {
            if (x <= 0 || x >= Ground.Length - 1) return 0f;
            float dy = Ground[x + 1] - Ground[x - 1];
            float dx = 2f;
            return (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
        }

        
        public bool IsBelowGround(float x, float y)
        {
            int ix = (int)Math.Max(0, Math.Min(x, Ground.Length - 1));
            return y >= Ground[ix];
        }

        
        public void CreateCrater(int centerX, int radius)
        {
            for (int x = centerX - radius; x <= centerX + radius; x++)
            {
                if (x < 0 || x >= Ground.Length) continue;
                int dx = x - centerX;
                int depth = (int)Math.Sqrt(radius * radius - dx * dx);
                depth = (int)(depth * 1.5f);
                Ground[x] = Math.Min(Ground[x] + depth, _panelHeight - 10);
            }
        }
    }
}
