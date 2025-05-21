using System.Windows.Forms;

namespace TestCases
{
    partial class ResponsiveDesign
    {
        
            private System.Windows.Forms.SplitContainer splitContainer;

            private void InitializeComponent()
            {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // split
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.LightBlue;
            // 
            // splitContainer.Panel2 
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer.Size = new System.Drawing.Size(800, 500);
            this.splitContainer.SplitterDistance = 570;
            this.splitContainer.TabIndex = 0;
            // 
            // ResponsiveDesign Ministry of Defence
            //                                     
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.splitContainer);
            this.Name = "ResponsiveDesign";                   //Mery watan 
            this.Text = "Responsive Split Form"; 
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false); //Shakeel 
            this.ResumeLayout(false);
            
            }
        }
    }