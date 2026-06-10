using OOP_GAME.BL;
using OOP_GAME.Model;
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

            // load saved appearance into session at startup
            CurrentSession.Instance.LoadAppearanceFromSettings();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            NetworkManager.Reset();
            Application.Exit();
        }

        private void GarageButton_Click(object sender, EventArgs e)
        {
            UI.Garage garageForm = new UI.Garage();
            garageForm.Show(this);
            this.Hide();
        }

        private async void HostButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Missing Username",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // setup session
            var session = CurrentSession.Instance;
            session.IsHost = true;
            session.LocalPlayerName = username;
            session.LoadAppearanceFromSettings();

            // disable buttons while waiting
            HostButton.Enabled = false;
            JoinButton.Enabled = false;
            StatusLabel.Text = "Waiting for opponent on port 8888...";
            StatusLabel.ForeColor = Color.Yellow;

            // reset and start hosting
            NetworkManager.Reset();
            await NetworkManager.Instance.StartHostAsync();

            // check if connection succeeded
            if (CurrentSession.Instance.RemotePlayerName != null)
            {
                StatusLabel.Text = $"Connected! {session.RemotePlayerName} joined.";
                StatusLabel.ForeColor = Color.LimeGreen;

                // short delay so user sees the connected message
                await Task.Delay(500);

                // launch game
                GameForm gameForm = new GameForm();
                gameForm.Show(this);
                this.Hide();
            }
            else
            {
                StatusLabel.Text = "Connection failed.";
                StatusLabel.ForeColor = Color.Red;
                HostButton.Enabled = true;
                JoinButton.Enabled = true;
            }
        }

        private async void JoinButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Missing Username",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ip = IPTextBox.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Please enter the host's IP address.", "Missing IP",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // setup session
            var session = CurrentSession.Instance;
            session.IsHost = false;
            session.LocalPlayerName = username;
            session.LoadAppearanceFromSettings();

            // disable buttons
            HostButton.Enabled = false;
            JoinButton.Enabled = false;
            StatusLabel.Text = $"Connecting to {ip}...";
            StatusLabel.ForeColor = Color.Yellow;

            // reset and connect
            NetworkManager.Reset();
            await NetworkManager.Instance.ConnectToHostAsync(ip);

            // check if we got the START message
            if (session.TerrainSeed != 0)
            {
                StatusLabel.Text = "Connected! Starting game...";
                StatusLabel.ForeColor = Color.LimeGreen;

                await Task.Delay(500);

                // launch game
                GameForm gameForm = new GameForm();
                gameForm.Show(this);
                this.Hide();
            }
            else
            {
                StatusLabel.Text = "Connection failed. Check IP and try again.";
                StatusLabel.ForeColor = Color.Red;
                HostButton.Enabled = true;
                JoinButton.Enabled = true;
            }
        }
    }
}
