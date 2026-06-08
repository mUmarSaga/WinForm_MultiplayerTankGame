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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ExitButton = new FontAwesome.Sharp.IconButton();
            this.OptionButton = new FontAwesome.Sharp.IconButton();
            this.GarageButton = new FontAwesome.Sharp.IconButton();
            this.PlayButton = new FontAwesome.Sharp.IconButton();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.ExitButton);
            this.panel2.Controls.Add(this.OptionButton);
            this.panel2.Controls.Add(this.GarageButton);
            this.panel2.Controls.Add(this.PlayButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 350);
            this.panel2.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.BackColor = System.Drawing.Color.Black;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.ForeColor = System.Drawing.SystemColors.Control;
            this.ExitButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.ExitButton.IconColor = System.Drawing.Color.Black;
            this.ExitButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ExitButton.Location = new System.Drawing.Point(371, 263);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(79, 35);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // OptionButton
            // 
            this.OptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionButton.BackColor = System.Drawing.Color.Black;
            this.OptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionButton.ForeColor = System.Drawing.SystemColors.Control;
            this.OptionButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.OptionButton.IconColor = System.Drawing.Color.Black;
            this.OptionButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.OptionButton.Location = new System.Drawing.Point(371, 187);
            this.OptionButton.Name = "OptionButton";
            this.OptionButton.Size = new System.Drawing.Size(79, 35);
            this.OptionButton.TabIndex = 2;
            this.OptionButton.Text = "Options";
            this.OptionButton.UseVisualStyleBackColor = false;
            // 
            // GarageButton
            // 
            this.GarageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.GarageButton.BackColor = System.Drawing.Color.Black;
            this.GarageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GarageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GarageButton.ForeColor = System.Drawing.SystemColors.Control;
            this.GarageButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.GarageButton.IconColor = System.Drawing.Color.Black;
            this.GarageButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GarageButton.Location = new System.Drawing.Point(371, 113);
            this.GarageButton.Name = "GarageButton";
            this.GarageButton.Size = new System.Drawing.Size(79, 35);
            this.GarageButton.TabIndex = 1;
            this.GarageButton.Text = "Garage";
            this.GarageButton.UseVisualStyleBackColor = false;
            this.GarageButton.Click += new System.EventHandler(this.GarageButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.PlayButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.PlayButton.IconColor = System.Drawing.Color.BlanchedAlmond;
            this.PlayButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.PlayButton.Location = new System.Drawing.Point(371, 38);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(79, 35);
            this.PlayButton.TabIndex = 0;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OOP_GAME.Properties.Resources.Gemini_Generated_Image_p84hk7p84hk7p84h;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton GarageButton;
        private FontAwesome.Sharp.IconButton PlayButton;
        private FontAwesome.Sharp.IconButton OptionButton;
        private FontAwesome.Sharp.IconButton ExitButton;
    }
}