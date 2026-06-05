using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_GAME.UI
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            UI.GameForm gameForm = new UI.GameForm();
            gameForm.Show(this);
            this.Hide();
        }

        private void GarageButton_Click(object sender, EventArgs e)
        {
            UI.Garage garageForm = new UI.Garage();
            garageForm.Show(this);
            this.Hide();
        }
    }
}
