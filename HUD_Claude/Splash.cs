using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUD_Claude
{
    public partial class Splash : Form
    {
        private Timer animationTimer;
        private float animationProgress = 0;
        private const float ANIMATION_SPEED = 0.02f;
        private const int TOTAL_DURATION = 3000; // 3 seconds

        // Logo colors
        private readonly Color logoColor = Color.FromArgb(0, 171, 142); // Turquoise/teal color

        // Points for the roof geometry
        private readonly Point[] leftRoofPoints;
        private readonly Point[] rightRoofPoints;

        public Splash()
        {
            // Form setup
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            // Calculate roof points based on form size
            int centerX = this.Width / 2;
            int topY = 50;

            // Adjusted points to match the actual logo geometry
            leftRoofPoints = new Point[]
            {
                new Point(centerX - 120, topY + 40),  // Bottom point
                new Point(centerX - 80, topY + 40),   // Middle wing
                new Point(centerX - 40, topY + 40),   // Inner wing
                new Point(centerX, topY)              // Peak
            };

            rightRoofPoints = new Point[]
            {
                new Point(centerX + 120, topY + 40),  // Bottom point
                new Point(centerX + 80, topY + 40),   // Middle wing
                new Point(centerX + 40, topY + 40),   // Inner wing
                new Point(centerX, topY)              // Peak
            };

            // Setup animation timer
            animationTimer = new Timer();
            animationTimer.Interval = 16; // Approximately 60 FPS
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            // Enable double buffering for smooth animation
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationProgress += ANIMATION_SPEED;

            if (animationProgress >= 1.0f)
            {
                animationTimer.Stop();
                // Optional: Add code here to close the splash screen or transition to main form
                // System.Threading.Thread.Sleep(1000); // Pause at the end
                // this.Close();
            }

            this.Invalidate(); // Trigger repaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen logoPen = new Pen(logoColor, 4)) // Increased pen width
            {
                // Draw left roof lines
                if (animationProgress > 0.1f)
                {
                    float leftProgress = Math.Min(1, (animationProgress - 0.1f) * 2);
                    DrawAnimatedLines(g, logoPen, leftRoofPoints, leftProgress);
                }

                // Draw right roof lines
                if (animationProgress > 0.3f)
                {
                    float rightProgress = Math.Min(1, (animationProgress - 0.3f) * 2);
                    DrawAnimatedLines(g, logoPen, rightRoofPoints, rightProgress);
                }
            }

            // Draw text with fade-in effect
            if (animationProgress > 0.5f)
            {
                float textAlpha = Math.Min(255, ((animationProgress - 0.5f) * 2) * 255);

                using (Font serbFont = new Font("Arial", 48, FontStyle.Bold)) // Increased font size
                using (Brush textBrush = new SolidBrush(Color.FromArgb((int)textAlpha, logoColor)))
                {
                    // Draw "SERB"
                    g.DrawString("SERB", serbFont, textBrush,
                        this.Width / 2 - g.MeasureString("SERB", serbFont).Width / 2,
                        this.Height / 2);

                    // Draw Arabic text
                    using (Font arabicFont = new Font("Arial", 36)) // Increased font size
                    {
                        g.DrawString("سرب", arabicFont, textBrush,
                            this.Width / 2 - g.MeasureString("سرب", arabicFont).Width / 2,
                            this.Height / 2 + 60);
                    }
                }
            }
        }

        private void DrawAnimatedLines(Graphics g, Pen pen, Point[] points, float progress)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                if (progress > (float)i / (points.Length - 1))
                {
                    float lineProgress = Math.Min(1, (progress - (float)i / (points.Length - 1)) * (points.Length - 1));
                    Point start = points[i];
                    Point end = points[i + 1];

                    Point animatedEnd = new Point(
                        (int)(start.X + (end.X - start.X) * lineProgress),
                        (int)(start.Y + (end.Y - start.Y) * lineProgress)
                    );

                    g.DrawLine(pen, start, animatedEnd);
                }
            }
        }
    }

}
