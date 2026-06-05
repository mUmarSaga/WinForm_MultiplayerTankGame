using OOP_GAME.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace OOP_GAME
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            image = System.Drawing.Image.FromFile("C:\\Users\\mertt\\Desktop\\OOP_GAME\\OOP_GAME\\Resources\\Tank.png");
            

        }
        int[] ground;
        Tank PlayerTank;
        System.Drawing.Image image;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g= e.Graphics;
            var terrainManager = new BL.TerrainManager(panel1.Width, panel1.Height);
            terrainManager.GenerateTerrain();
            ground = terrainManager.Ground;
            PlayerTank = new LightTank("Player",1);
            Point[] graphicsPoint = new Point[panel1.Width+2];

            for (int x = 0; x < panel1.Width; x++) { 
                    graphicsPoint[x] = new Point(x, ground[x]);
            }
            graphicsPoint[ground.Length-1] = new Point(panel1.Width-1, panel1.Height);
            graphicsPoint[ground.Length] = new Point(0, panel1.Height);

            g.FillPolygon(Brushes.Green, graphicsPoint);
            g.DrawPolygon(Pens.Black, graphicsPoint);
            //g.DrawImage(PlayerTank.TankImage, PlayerTank.x, PlayerTank.y - PlayerTank.TankImage.Height);
        }
    }
}
