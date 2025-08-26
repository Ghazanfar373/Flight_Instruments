namespace HUD_Claude
{
    partial class SwarmHud
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.horizonIndicator1 = new HUD_Claude.HorizonIndicator();
            this.compassControl1 = new HUD_Claude.CompassControl();
            this.labelVehicle = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonArm = new System.Windows.Forms.Button();
            this.buttonLoiter = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.flowLayoutPanel1.Controls.Add(this.horizonIndicator1);
            this.flowLayoutPanel1.Controls.Add(this.compassControl1);
            this.flowLayoutPanel1.Controls.Add(this.labelVehicle);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(128, 271);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // horizonIndicator1
            // 
            this.horizonIndicator1.BackColor = System.Drawing.Color.Transparent;
            this.horizonIndicator1.Location = new System.Drawing.Point(3, 0);
            this.horizonIndicator1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.horizonIndicator1.Name = "horizonIndicator1";
            this.horizonIndicator1.Pitch = 0F;
            this.horizonIndicator1.Roll = 0F;
            this.horizonIndicator1.Size = new System.Drawing.Size(120, 120);
            this.horizonIndicator1.TabIndex = 0;
            // 
            // compassControl1
            // 
            this.compassControl1.Heading = 0F;
            this.compassControl1.Location = new System.Drawing.Point(3, 120);
            this.compassControl1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.compassControl1.Name = "compassControl1";
            this.compassControl1.Size = new System.Drawing.Size(120, 110);
            this.compassControl1.TabIndex = 1;
            // 
            // labelVehicle
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.labelVehicle, true);
            this.labelVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVehicle.ForeColor = System.Drawing.SystemColors.Control;
            this.labelVehicle.Location = new System.Drawing.Point(3, 230);
            this.labelVehicle.Name = "labelVehicle";
            this.labelVehicle.Size = new System.Drawing.Size(120, 16);
            this.labelVehicle.TabIndex = 2;
            this.labelVehicle.Text = "Vehicle 1";
            this.labelVehicle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonArm);
            this.flowLayoutPanel2.Controls.Add(this.buttonLoiter);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 247);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(127, 22);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // buttonArm
            // 
            this.buttonArm.Location = new System.Drawing.Point(2, 1);
            this.buttonArm.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.buttonArm.Name = "buttonArm";
            this.buttonArm.Size = new System.Drawing.Size(60, 23);
            this.buttonArm.TabIndex = 0;
            this.buttonArm.Text = "Arm";
            this.buttonArm.UseVisualStyleBackColor = true;
            // 
            // buttonLoiter
            // 
            this.buttonLoiter.Location = new System.Drawing.Point(64, 1);
            this.buttonLoiter.Margin = new System.Windows.Forms.Padding(1);
            this.buttonLoiter.Name = "buttonLoiter";
            this.buttonLoiter.Size = new System.Drawing.Size(60, 23);
            this.buttonLoiter.TabIndex = 1;
            this.buttonLoiter.Text = "Loiter";
            this.buttonLoiter.UseVisualStyleBackColor = true;
            // 
            // SwarmHud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SwarmHud";
            this.Size = new System.Drawing.Size(132, 275);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private HorizonIndicator horizonIndicator1;
        private CompassControl compassControl1;
        private System.Windows.Forms.Label labelVehicle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonArm;
        private System.Windows.Forms.Button buttonLoiter;
    }
}
