using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace HUD_Claude
{


    public partial class AnimatedForm : Form
    {
        private Timer timer;
        private Bitmap image;
        private int angle;
        private float scale;
        private bool zoomingIn;
        public AnimatedForm()
        {
            this.DoubleBuffered = true;
            this.image = new Bitmap("D:\\Icons Store\\Pictures\\serb_logo500x500.png");
            this.ClientSize=new Size(600,400);
            this.timer = new Timer();
            this.timer.Interval = 50; // Animation interval in milliseconds
            this.timer.Tick += new EventHandler(this.OnTick);
            this.timer.Start();
            this.scale = 1.5f;
            this.zoomingIn = true;
        }

        private void OnTick(object sender, EventArgs e)
        {

            //scale = 2.0f;
            if (zoomingIn)
            //{
            //    scale += 0.05f; // Increase scale
            //    if (scale >= 2.0f)
            //    {
            //        zoomingIn = false;
            //    }
            //}
            //else
            {
                scale -= 0.03f; // Decrease scale
                if (scale <= 0.5f)
                {
                    zoomingIn = false;
                }
            }

            this.Invalidate(); // Trigger a redraw
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            // Calculate the center of the form
            PointF center = new PointF(this.ClientSize.Width / 2, this.ClientSize.Height / 2);

            // Calculate the dimensions of the zoomed image
            float width = this.image.Width * scale;
            float height = this.image.Height * scale;

            // Draw the zoomed image centered in the form
            g.DrawImage(this.image, center.X - width / 2, center.Y - height / 2, width, height);
        }
    }
}
