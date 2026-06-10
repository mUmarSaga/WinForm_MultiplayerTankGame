namespace OOP_GAME.UI
{
    partial class Garage
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
            this.BodyNext = new System.Windows.Forms.PictureBox();
            this.CannonNext = new System.Windows.Forms.PictureBox();
            this.BodyPrevious = new System.Windows.Forms.PictureBox();
            this.CannonPrevious = new System.Windows.Forms.PictureBox();
            this.SelectButton = new System.Windows.Forms.PictureBox();
            this.Body = new System.Windows.Forms.PictureBox();
            this.Cannon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BodyNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CannonNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BodyPrevious)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CannonPrevious)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cannon)).BeginInit();
            this.SuspendLayout();
            // 
            // BodyNext
            // 
            this.BodyNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BodyNext.BackColor = System.Drawing.Color.Transparent;
            this.BodyNext.BackgroundImage = global::OOP_GAME.Properties.Resources.arrowButton_removebg_preview1;
            this.BodyNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BodyNext.Location = new System.Drawing.Point(1206, 572);
            this.BodyNext.Name = "BodyNext";
            this.BodyNext.Size = new System.Drawing.Size(67, 61);
            this.BodyNext.TabIndex = 0;
            this.BodyNext.TabStop = false;
            this.BodyNext.Click += new System.EventHandler(this.BodyNext_Click);
            // 
            // CannonNext
            // 
            this.CannonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CannonNext.BackColor = System.Drawing.Color.Transparent;
            this.CannonNext.BackgroundImage = global::OOP_GAME.Properties.Resources.arrowButton_removebg_preview1;
            this.CannonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CannonNext.Location = new System.Drawing.Point(1206, 325);
            this.CannonNext.Name = "CannonNext";
            this.CannonNext.Size = new System.Drawing.Size(67, 61);
            this.CannonNext.TabIndex = 1;
            this.CannonNext.TabStop = false;
            this.CannonNext.Click += new System.EventHandler(this.CannonNext_Click);
            // 
            // BodyPrevious
            // 
            this.BodyPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BodyPrevious.BackColor = System.Drawing.Color.Transparent;
            this.BodyPrevious.BackgroundImage = global::OOP_GAME.Properties.Resources.arrowButton_inverted;
            this.BodyPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BodyPrevious.Location = new System.Drawing.Point(81, 572);
            this.BodyPrevious.Name = "BodyPrevious";
            this.BodyPrevious.Size = new System.Drawing.Size(67, 61);
            this.BodyPrevious.TabIndex = 2;
            this.BodyPrevious.TabStop = false;
            this.BodyPrevious.Click += new System.EventHandler(this.BodyPrevious_Click);
            // 
            // CannonPrevious
            // 
            this.CannonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CannonPrevious.BackColor = System.Drawing.Color.Transparent;
            this.CannonPrevious.BackgroundImage = global::OOP_GAME.Properties.Resources.arrowButton_inverted;
            this.CannonPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CannonPrevious.Location = new System.Drawing.Point(81, 325);
            this.CannonPrevious.Name = "CannonPrevious";
            this.CannonPrevious.Size = new System.Drawing.Size(67, 61);
            this.CannonPrevious.TabIndex = 3;
            this.CannonPrevious.TabStop = false;
            this.CannonPrevious.Click += new System.EventHandler(this.CannonPrevious_Click_1);
            // 
            // SelectButton
            // 
            this.SelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectButton.BackColor = System.Drawing.Color.Transparent;
            this.SelectButton.BackgroundImage = global::OOP_GAME.Properties.Resources.SelectButton_removebg_preview;
            this.SelectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SelectButton.Location = new System.Drawing.Point(360, 667);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(637, 61);
            this.SelectButton.TabIndex = 4;
            this.SelectButton.TabStop = false;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // Body
            // 
            this.Body.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Body.BackColor = System.Drawing.Color.Transparent;
            this.Body.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Body.Location = new System.Drawing.Point(220, 392);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(954, 254);
            this.Body.TabIndex = 5;
            this.Body.TabStop = false;
            // 
            // Cannon
            // 
            this.Cannon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cannon.BackColor = System.Drawing.Color.Transparent;
            this.Cannon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cannon.Location = new System.Drawing.Point(220, 168);
            this.Cannon.Name = "Cannon";
            this.Cannon.Size = new System.Drawing.Size(954, 218);
            this.Cannon.TabIndex = 6;
            this.Cannon.TabStop = false;
            // 
            // Garage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OOP_GAME.Properties.Resources.Gemini_Generated_Image_hkdl7ehkdl7ehkdl;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.ControlBox = false;
            this.Controls.Add(this.Cannon);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.CannonPrevious);
            this.Controls.Add(this.BodyPrevious);
            this.Controls.Add(this.CannonNext);
            this.Controls.Add(this.BodyNext);
            this.DoubleBuffered = true;
            this.Name = "Garage";
            this.Text = "Garage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Garage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BodyNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CannonNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BodyPrevious)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CannonPrevious)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cannon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BodyNext;
        private System.Windows.Forms.PictureBox CannonNext;
        private System.Windows.Forms.PictureBox BodyPrevious;
        private System.Windows.Forms.PictureBox CannonPrevious;
        private System.Windows.Forms.PictureBox SelectButton;
        private System.Windows.Forms.PictureBox Body;
        private System.Windows.Forms.PictureBox Cannon;
    }
}