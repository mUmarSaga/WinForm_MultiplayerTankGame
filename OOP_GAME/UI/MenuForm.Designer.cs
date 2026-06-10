namespace OOP_GAME.UI
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.ExitButton = new FontAwesome.Sharp.IconButton();
            this.GarageButton = new FontAwesome.Sharp.IconButton();
            this.JoinButton = new FontAwesome.Sharp.IconButton();
            this.HostButton = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.TitleLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 80);
            this.panel1.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Impact", 28F, System.Drawing.FontStyle.Bold);
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(250, 20);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(336, 46);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "TANK BATTLE ONLINE";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.StatusLabel);
            this.panel2.Controls.Add(this.IPTextBox);
            this.panel2.Controls.Add(this.IPLabel);
            this.panel2.Controls.Add(this.UsernameTextBox);
            this.panel2.Controls.Add(this.UsernameLabel);
            this.panel2.Controls.Add(this.ExitButton);
            this.panel2.Controls.Add(this.GarageButton);
            this.panel2.Controls.Add(this.JoinButton);
            this.panel2.Controls.Add(this.HostButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 370);
            this.panel2.TabIndex = 1;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
            this.StatusLabel.ForeColor = System.Drawing.Color.LightGray;
            this.StatusLabel.Location = new System.Drawing.Point(200, 310);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(400, 30);
            this.StatusLabel.TabIndex = 14;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IPTextBox
            // 
            this.IPTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IPTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.IPTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IPTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.IPTextBox.ForeColor = System.Drawing.Color.White;
            this.IPTextBox.Location = new System.Drawing.Point(340, 40);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(200, 29);
            this.IPTextBox.TabIndex = 13;
            this.IPTextBox.Text = "127.0.0.1";
            // 
            // IPLabel
            // 
            this.IPLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IPLabel.AutoSize = true;
            this.IPLabel.BackColor = System.Drawing.Color.Transparent;
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.IPLabel.ForeColor = System.Drawing.Color.White;
            this.IPLabel.Location = new System.Drawing.Point(240, 45);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(74, 20);
            this.IPLabel.TabIndex = 12;
            this.IPLabel.Text = "Host IP:";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.UsernameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.UsernameTextBox.ForeColor = System.Drawing.Color.White;
            this.UsernameTextBox.Location = new System.Drawing.Point(340, 5);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(200, 29);
            this.UsernameTextBox.TabIndex = 11;
            this.UsernameTextBox.Text = "Player";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.UsernameLabel.ForeColor = System.Drawing.Color.White;
            this.UsernameLabel.Location = new System.Drawing.Point(240, 10);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(96, 20);
            this.UsernameLabel.TabIndex = 10;
            this.UsernameLabel.Text = "Username:";
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.ExitButton.IconColor = System.Drawing.Color.Black;
            this.ExitButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ExitButton.Location = new System.Drawing.Point(280, 255);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(220, 40);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // GarageButton
            // 
            this.GarageButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GarageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.GarageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GarageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GarageButton.ForeColor = System.Drawing.Color.White;
            this.GarageButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.GarageButton.IconColor = System.Drawing.Color.Black;
            this.GarageButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GarageButton.Location = new System.Drawing.Point(280, 200);
            this.GarageButton.Name = "GarageButton";
            this.GarageButton.Size = new System.Drawing.Size(220, 40);
            this.GarageButton.TabIndex = 2;
            this.GarageButton.Text = "Garage";
            this.GarageButton.UseVisualStyleBackColor = false;
            this.GarageButton.Click += new System.EventHandler(this.GarageButton_Click);
            // 
            // JoinButton
            // 
            this.JoinButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.JoinButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(120)))));
            this.JoinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.JoinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinButton.ForeColor = System.Drawing.Color.White;
            this.JoinButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.JoinButton.IconColor = System.Drawing.Color.Black;
            this.JoinButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.JoinButton.Location = new System.Drawing.Point(280, 145);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(220, 40);
            this.JoinButton.TabIndex = 1;
            this.JoinButton.Text = "Join Game";
            this.JoinButton.UseVisualStyleBackColor = false;
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            // 
            // HostButton
            // 
            this.HostButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HostButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.HostButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HostButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HostButton.ForeColor = System.Drawing.Color.White;
            this.HostButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.HostButton.IconColor = System.Drawing.Color.Black;
            this.HostButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.HostButton.Location = new System.Drawing.Point(280, 90);
            this.HostButton.Name = "HostButton";
            this.HostButton.Size = new System.Drawing.Size(220, 40);
            this.HostButton.TabIndex = 0;
            this.HostButton.Text = "Host Game";
            this.HostButton.UseVisualStyleBackColor = false;
            this.HostButton.Click += new System.EventHandler(this.HostButton_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OOP_GAME.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "MenuForm";
            this.Text = "Tank Battle Online";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private FontAwesome.Sharp.IconButton HostButton;
        private FontAwesome.Sharp.IconButton JoinButton;
        private FontAwesome.Sharp.IconButton GarageButton;
        private FontAwesome.Sharp.IconButton ExitButton;
        private System.Windows.Forms.Label StatusLabel;
    }
}