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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.apStatusControl2 = new HUD_Claude.APStatusControl();
            this.apStatusControl1 = new HUD_Claude.APStatusControl();
            this.horizonIndicator21 = new HUD_Claude.HorizonIndicator2();
            this.swarmHud1 = new HUD_Claude.SwarmHud();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripProgressBar1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(768, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 28);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Change Status";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // apStatusControl2
            // 
            this.apStatusControl2.AutopilotMode = "MANUAL";
            this.apStatusControl2.IsArmed = false;
            this.apStatusControl2.Location = new System.Drawing.Point(269, 219);
            this.apStatusControl2.Name = "apStatusControl2";
            this.apStatusControl2.Size = new System.Drawing.Size(147, 23);
            this.apStatusControl2.TabIndex = 6;
            // 
            // apStatusControl1
            // 
            this.apStatusControl1.AutopilotMode = "MANUAL";
            this.apStatusControl1.IsArmed = false;
            this.apStatusControl1.Location = new System.Drawing.Point(317, 77);
            this.apStatusControl1.Name = "apStatusControl1";
            this.apStatusControl1.Size = new System.Drawing.Size(149, 25);
            this.apStatusControl1.TabIndex = 5;
            // 
            // horizonIndicator21
            // 
            this.horizonIndicator21.BackColor = System.Drawing.Color.Transparent;
            this.horizonIndicator21.Location = new System.Drawing.Point(472, 64);
            this.horizonIndicator21.Name = "horizonIndicator21";
            this.horizonIndicator21.Pitch = 0F;
            this.horizonIndicator21.Roll = 0F;
            this.horizonIndicator21.Size = new System.Drawing.Size(207, 220);
            this.horizonIndicator21.TabIndex = 4;
            // 
            // swarmHud1
            // 
            this.swarmHud1.ApMode = "MANUAL";
            this.swarmHud1.ArmButtonEnabled = true;
            this.swarmHud1.ArmButtonText = "Arm";
            this.swarmHud1.Heading = 0F;
            this.swarmHud1.isArm = false;
            this.swarmHud1.Location = new System.Drawing.Point(116, 108);
            this.swarmHud1.LoiterButtonEnabled = true;
            this.swarmHud1.LoiterButtonText = "Loiter";
            this.swarmHud1.Name = "swarmHud1";
            this.swarmHud1.Pitch = 0F;
            this.swarmHud1.Roll = 0F;
            this.swarmHud1.Size = new System.Drawing.Size(132, 291);
            this.swarmHud1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(768, 442);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.apStatusControl2);
            this.Controls.Add(this.apStatusControl1);
            this.Controls.Add(this.horizonIndicator21);
            this.Controls.Add(this.swarmHud1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private HUD_Claude.SwarmHud swarmHud1;
        private HUD_Claude.HorizonIndicator2 horizonIndicator21;
        private HUD_Claude.APStatusControl apStatusControl1;
        private HUD_Claude.APStatusControl apStatusControl2;
        private System.Windows.Forms.Button button1;
    }
}

