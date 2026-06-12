using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_GAME.Model;
using System.Windows.Forms;

namespace OOP_GAME.UI
{
    public partial class Garage : Form
    {
        private string[] _bodyImages = {
                "assets/heavyTank1Body.png",
                "assets/heavyTank2Body.png",
                "assets/heavyTank3Body.png"
            };

        private string[] _cannonImages = {
            "assets/heavyTank1Cannon.png",
            "assets/heavyTank2Cannon.png",
            "assets/heavyTank3Cannon.png"
        };

        private int _bodyIndex = 0;
        private int _cannonIndex = 0;

        public Garage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Garage_Load(object sender, EventArgs e)
        {
            
            _bodyIndex = Properties.Settings.Default.BodySpriteIndex;
            _cannonIndex = Properties.Settings.Default.CannonSpriteIndex;

            
            _bodyIndex = Math.Max(0, Math.Min(_bodyIndex, _bodyImages.Length - 1));
            _cannonIndex = Math.Max(0, Math.Min(_cannonIndex, _cannonImages.Length - 1));

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            Body.Image = Image.FromFile(_bodyImages[_bodyIndex]);
            Cannon.Image = Image.FromFile(_cannonImages[_cannonIndex]);
            this.Text = "Garage — Customize Your Tank";
        }

        private void CannonPrevious_Click_1(object sender, EventArgs e)
        {
            _cannonIndex = (_cannonIndex - 1 + _cannonImages.Length) % _cannonImages.Length;
            UpdateDisplay();
        }

        private void BodyPrevious_Click(object sender, EventArgs e)
        {
            _bodyIndex = (_bodyIndex - 1 + _bodyImages.Length) % _bodyImages.Length;
            UpdateDisplay();
        }

        private void CannonNext_Click(object sender, EventArgs e)
        {
            _cannonIndex = (_cannonIndex + 1) % _cannonImages.Length;
            UpdateDisplay();
        }

        private void BodyNext_Click(object sender, EventArgs e)
        {
            _bodyIndex = (_bodyIndex + 1) % _bodyImages.Length;
            UpdateDisplay();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.BodySpriteIndex = _bodyIndex;
            Properties.Settings.Default.CannonSpriteIndex = _cannonIndex;
            Properties.Settings.Default.Save();

            
            CurrentSession.Instance.LocalBodyIndex = _bodyIndex;
            CurrentSession.Instance.LocalCannonIndex = _cannonIndex;

            
            if (this.Owner != null)
                this.Owner.Show();
            this.Close();
        }
    }
}
