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
        private int _currentPlayer = 1; // 1 or 2
        public Garage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
        }
        private void UpdateDisplay()
        {
            Body.Image = Image.FromFile(_bodyImages[_bodyIndex]);
            Cannon.Image = Image.FromFile(_cannonImages[_cannonIndex]);

            // show which player is selecting
            this.Text = $"Player {_currentPlayer} - Select Your Tank";
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
            if (_currentPlayer == 1)
            {
                // save player 1 selection
                CurrentSession.Instance.Player1BodyImage = _bodyImages[_bodyIndex];
                CurrentSession.Instance.Player1CannonImage = _cannonImages[_cannonIndex];

                // reset for player 2
                _bodyIndex = 0;
                _cannonIndex = 0;
                _currentPlayer = 2;
                UpdateDisplay();
            }
            else
            {
                // save player 2 selection
                CurrentSession.Instance.Player2BodyImage = _bodyImages[_bodyIndex];
                CurrentSession.Instance.Player2CannonImage = _cannonImages[_cannonIndex];

                // go to game
                var game = new GameForm();
                game.Show(this.Owner);
                this.Close();
            }
        }

        private void Garage_Load(object sender, EventArgs e)
        {
            
        }


    }
}
