using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUD_Claude
{
    public partial class MainForm : Form
    {
        private float pitch = 0; // Degrees, positive is nose up
        private float roll = 0;  // Degrees, positive is right roll
        public MainForm()
        {
            InitializeComponent();
         
            this.DoubleBuffered = true;

            

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    pitch = Math.Min(pitch + 1, 90);
                    break;
                case Keys.Down:
                    pitch = Math.Max(pitch - 1, -90);
                    break;
                case Keys.Left:
                    roll = Math.Max(roll - 1, -180);
                    break;
                case Keys.Right:
                    roll = Math.Min(roll + 1, 180);
                    break;
                case Keys.Space:
                    pitch = 0;
                    roll = 0;
                    break;
            }
            hudControl1.Pitch = pitch;
            hudControl1.Roll = roll;
            compassControl1.Heading = pitch;

            horizonIndicator1.Pitch = pitch;
            horizonIndicator1.Roll = roll;  
            return base.ProcessCmdKey(ref msg, keyData);
          
        }

       
    }
}
