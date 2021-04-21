namespace KTN
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.components = new System.ComponentModel.Container();
            this.player = new System.Windows.Forms.PictureBox();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.player.BackgroundImage = global::KTN.Resources.turbokiller;
            this.player.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.player.Location = new System.Drawing.Point(64, 64);
            this.player.Margin = new System.Windows.Forms.Padding(0);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(64, 64);
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.MainGameTimer);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::KTN.Resources.asphalt;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.player);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize) (this.player)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Timer GameTimer;

        private System.Windows.Forms.PictureBox player;

        #endregion
    }
}