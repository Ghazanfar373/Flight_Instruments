namespace TopographySRTM
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
            this.buttonConvert = new System.Windows.Forms.Button();
            this.hudControl1 = new HUD_Claude.HUDControl();
            this.SuspendLayout();
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(304, 100);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonConvert.TabIndex = 0;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.ButtonConvert_Click);
            // 
            // hudControl1
            // 
            this.hudControl1.Location = new System.Drawing.Point(682, 1);
            this.hudControl1.Name = "hudControl1";
            this.hudControl1.Size = new System.Drawing.Size(405, 412);
            this.hudControl1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 536);
            this.Controls.Add(this.hudControl1);
            this.Controls.Add(this.buttonConvert);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonConvert;
        private HUD_Claude.HUDControl hudControl1;
    }
}

