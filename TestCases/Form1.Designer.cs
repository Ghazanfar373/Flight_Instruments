namespace TestCases
{
    partial class Form1
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
            this.compassControl1 = new HUD_Claude.CompassControl();
            this.SuspendLayout();
            // 
            // compassControl1
            // 
            this.compassControl1.Heading = 0F;
            this.compassControl1.Location = new System.Drawing.Point(90, 110);
            this.compassControl1.Name = "compassControl1";
            this.compassControl1.Size = new System.Drawing.Size(156, 153);
            this.compassControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 352);
            this.Controls.Add(this.compassControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private HUD_Claude.CompassControl compassControl1;
    }
}

