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
        private Timer updateTimer;
        private float pitch = 0; // Degrees, positive is nose up
        private float roll = 0;  // Degrees, positive is right roll
        public MainForm()
        {
            InitializeComponent();
            
            //this.ClientSize = new System.Drawing.Size(1013, 450);
            this.DoubleBuffered = true;

            //updateTimer = new Timer();
            //updateTimer.Interval = 50; // 20 FPS
            //updateTimer.Tick += UpdateTimer_Tick;
            //updateTimer.Start();

            //this.KeyPreview = true;
            //this.KeyDown += MainForm_KeyDown;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    pitch = Math.Min(pitch + 5, 90);
                    break;
                case Keys.Down:
                    pitch = Math.Max(pitch - 5, -90);
                    break;
                case Keys.Left:
                    roll = Math.Max(roll - 5, -180);
                    break;
                case Keys.Right:
                    roll = Math.Min(roll + 5, 180);
                    break;
                case Keys.Space:
                    pitch = 0;
                    roll = 0;
                    break;
            }
            hudControl1.Pitch = pitch;
            hudControl1.Roll = roll;
            compassControl1.Heading = pitch;
            return base.ProcessCmdKey(ref msg, keyData);
           

            //System.Diagnostics.Debug.WriteLine(pitch);
            //compassControl1.Invalidate();
        }

       
    }
}
