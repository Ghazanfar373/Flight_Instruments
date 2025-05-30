﻿namespace TestCases
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
            this.horizonIndicator1 = new HUD_Claude.HorizonIndicator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // compassControl1
            // 
            this.compassControl1.BackColor = System.Drawing.Color.Black;
            this.compassControl1.Heading = 0F;
            this.compassControl1.Location = new System.Drawing.Point(3, 3);
            this.compassControl1.Name = "compassControl1";
            this.compassControl1.Size = new System.Drawing.Size(307, 295);
            this.compassControl1.TabIndex = 0;
            // 
            // horizonIndicator1
            // 
            this.horizonIndicator1.Location = new System.Drawing.Point(316, 3);
            this.horizonIndicator1.Name = "horizonIndicator1";
            this.horizonIndicator1.Pitch = 0F;
            this.horizonIndicator1.Roll = 0F;
            this.horizonIndicator1.Size = new System.Drawing.Size(307, 295);
            this.horizonIndicator1.TabIndex = 1;
            this.horizonIndicator1.Load += new System.EventHandler(this.horizonIndicator1_Load);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.compassControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.horizonIndicator1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 301);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 301);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HUD_Claude.CompassControl compassControl1;
        private HUD_Claude.HorizonIndicator horizonIndicator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

