namespace HUD_Claude
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.horizonIndicator1 = new HUD_Claude.HorizonIndicator();
            this.hudControl1 = new HUD_Claude.HUDControl();
            this.compassControl1 = new HUD_Claude.CompassControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 453);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "* Use Keyboard Arrow Keys for inputs";
            // 
            // horizonIndicator1
            // 
            this.horizonIndicator1.Location = new System.Drawing.Point(872, 53);
            this.horizonIndicator1.MinimumSize = new System.Drawing.Size(200, 200);
            this.horizonIndicator1.Name = "horizonIndicator1";
            this.horizonIndicator1.Pitch = 0F;
            this.horizonIndicator1.Roll = 0F;
            this.horizonIndicator1.Size = new System.Drawing.Size(250, 250);
            this.horizonIndicator1.TabIndex = 6;
            // 
            // hudControl1
            // 
            this.hudControl1.Location = new System.Drawing.Point(12, 27);
            this.hudControl1.Name = "hudControl1";
            this.hudControl1.Size = new System.Drawing.Size(407, 398);
            this.hudControl1.TabIndex = 4;
            // 
            // compassControl1
            // 
            this.compassControl1.BackColor = System.Drawing.Color.Black;
            this.compassControl1.ForeColor = System.Drawing.Color.White;
            this.compassControl1.Heading = 0F;
            this.compassControl1.Location = new System.Drawing.Point(872, 295);
            this.compassControl1.Name = "compassControl1";
            this.compassControl1.Size = new System.Drawing.Size(250, 250);
            this.compassControl1.TabIndex = 0;
            this.compassControl1.Text = "compassControl1";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1216, 623);
            this.Controls.Add(this.horizonIndicator1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hudControl1);
            this.Controls.Add(this.compassControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CompassControl compassControl1;
        private HUDControl hudControl1;
        private System.Windows.Forms.Label label1;
        private HorizonIndicator horizonIndicator1;
    }
}