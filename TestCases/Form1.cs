using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCases
{
    public partial class Form1 : Form
    {

        private float pitch = 0; // Degrees, positive is nose up
        private float roll = 0;  // Degrees, positive is right roll
        public Form1()
        {
            InitializeComponent();
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
           
           swarmHud1.Pitch= pitch; swarmHud1.Roll= roll;
            swarmHud1.Heading= roll;
            swarmHud1.VehicleName = "Plane 2";
            
            
           
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void horizonIndicator1_Load(object sender, EventArgs e)
        {

        }
    }
}
