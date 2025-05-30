﻿using System;
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
           
            compassControl1.Heading = roll;
            horizonIndicator1.Pitch = pitch;
            horizonIndicator1.Roll = roll;
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void horizonIndicator1_Load(object sender, EventArgs e)
        {

        }
    }
}
