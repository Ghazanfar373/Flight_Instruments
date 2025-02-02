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
    public partial class HUD : Form
    {

        private float pitch = 0; // Degrees, positive is nose up
        private float roll = 0;  // Degrees, positive is right roll
        private Timer updateTimer;
        private const int CENTER_X = 200;
        private const int CENTER_Y = 200;
        private const int RADIUS = 150;
        private const float PITCH_SCALING = 4; // Pixels per degree of pitch

        public HUD()
        {
            
          
                this.ClientSize = new Size(400, 400);
                this.DoubleBuffered = true;

                updateTimer = new Timer();
                updateTimer.Interval = 50; // 20 FPS
                updateTimer.Tick += UpdateTimer_Tick;
                updateTimer.Start();

                this.KeyPreview = true;
                this.KeyDown += HorizonIndicator_KeyDown;
            }

            private void HorizonIndicator_KeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode)
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
                this.Invalidate();
            }

            private void UpdateTimer_Tick(object sender, EventArgs e)
            {
                this.Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Black);

                // Save the current state
                var state = g.Save();

                // Move to center
                g.TranslateTransform(CENTER_X, CENTER_Y);

                // Apply roll rotation
                g.RotateTransform(-roll);

                // Calculate pitch offset
                float pitchOffset = pitch * PITCH_SCALING;

                // Create clip region for horizon mask
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(-RADIUS, -RADIUS, RADIUS * 2, RADIUS * 2);
                    g.SetClip(path);
                }

                // Draw sky and ground
                using (var skyBrush = new SolidBrush(Color.SkyBlue))
                using (var groundBrush = new SolidBrush(Color.SaddleBrown))
                {
                    // Fill sky (top half)
                    g.FillRectangle(skyBrush, -RADIUS, -RADIUS - pitchOffset, RADIUS * 2, RADIUS * 2);

                    // Fill ground (bottom half)
                    g.FillRectangle(groundBrush, -RADIUS, -pitchOffset, RADIUS * 2, RADIUS * 2);
                }

                // Draw pitch lines
                using (var pen = new Pen(Color.White, 2))
                {
                    // Draw horizon line
                    g.DrawLine(pen, -RADIUS, -pitchOffset, RADIUS, -pitchOffset);

                    // Draw pitch reference lines
                    for (int i = -90; i <= 90; i += 10)
                    {
                        if (i == 0) continue; // Skip horizon line

                        float y = -pitchOffset - (i * PITCH_SCALING);

                        // Only draw lines that are within view
                        if (y > -RADIUS && y < RADIUS)
                        {
                            int lineLength = (i % 30 == 0) ? 60 : 30;
                            g.DrawLine(pen, -lineLength, y, lineLength, y);

                            // Draw degree numbers
                            using (var font = new Font("Arial", 8))
                            {
                                string text = Math.Abs(i).ToString();
                                g.DrawString(text, font, Brushes.White, -lineLength - 20, y - 6);
                                g.DrawString(text, font, Brushes.White, lineLength + 5, y - 6);
                            }
                        }
                    }
                }

                // Restore graphics state
                g.Restore(state);

                // Draw fixed elements after restoring the state
                // Draw circular mask
                using (var pen = new Pen(Color.White, 3))
                {
                    g.DrawEllipse(pen, CENTER_X - RADIUS, CENTER_Y - RADIUS, RADIUS * 2, RADIUS * 2);
                }

                // Draw fixed aircraft reference
                using (var pen = new Pen(Color.Yellow, 3))
                {
                    g.DrawLine(pen, CENTER_X - 50, CENTER_Y, CENTER_X - 15, CENTER_Y);
                    g.DrawLine(pen, CENTER_X + 15, CENTER_Y, CENTER_X + 50, CENTER_Y);
                    g.DrawLine(pen, CENTER_X, CENTER_Y - 15, CENTER_X, CENTER_Y + 15);
                }

            // Draw roll indicator arc and marks
            using (var pen = new Pen(Color.White, 2))
            {
                // Draw roll arc inside the horizon at the top
                int arcRadius = RADIUS - 20; // Slightly smaller than main horizon circle
                g.DrawArc(pen, CENTER_X - arcRadius, CENTER_Y - arcRadius, arcRadius * 2, arcRadius * 2, 210, 120);

                // Draw roll markers
                for (int angle = -60; angle <= 60; angle += 30)
                {
                    double radians = (angle + 90) * Math.PI / 180; // Offset by 90 to center at top
                    float markerLength = 10;
                    float x1 = CENTER_X + (float)((arcRadius - markerLength) * Math.Cos(radians));
                    float y1 = CENTER_Y + (float)((arcRadius - markerLength) * Math.Sin(radians));
                    float x2 = CENTER_X + (float)(arcRadius * Math.Cos(radians));
                    float y2 = CENTER_Y + (float)(arcRadius * Math.Sin(radians));
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
            // Draw roll indicator arc and marks
            using (var pen = new Pen(Color.White, 2))
            {
                // Draw roll arc inside the horizon at the top
                int arcRadius = RADIUS - 20; // Slightly smaller than main horizon circle
                g.DrawArc(pen, CENTER_X - arcRadius, CENTER_Y - arcRadius, arcRadius * 2, arcRadius * 2, 210, 120);

                // Draw roll markers and labels
                for (int angle = -60; angle <= 60; angle += 10) // Changed to 10-degree increments
                {
                    double radians = (angle + 90) * Math.PI / 180; // Offset by 90 to center at top
                    float markerLength;

                    // Vary marker length based on angle
                    if (angle == 0)
                    {
                        markerLength = 15; // Longer marker for center
                    }
                    else if (angle % 30 == 0)
                    {
                        markerLength = 12; // Medium length for 30-degree marks
                    }
                    else
                    {
                        markerLength = 8;  // Short length for 10-degree marks
                    }

                    // Draw the marker line
                    float x1 = CENTER_X - (float)((arcRadius - markerLength) * Math.Cos(radians));
                    float y1 = CENTER_Y - (float)((arcRadius - markerLength) * Math.Sin(radians));
                    float x2 = CENTER_X - (float)(arcRadius * Math.Cos(radians));
                    float y2 = CENTER_Y - (float)(arcRadius * Math.Sin(radians));
                    g.DrawLine(pen, x1, y1, x2, y2);

                    // Add labels for 30-degree markers
                    if (angle % 30 == 0 && angle != 0)
                    {
                        using (var font = new Font("Arial", 8))
                        {
                            string text = Math.Abs(angle).ToString();
                            float textX = CENTER_X - (float)((arcRadius - markerLength - 15) * Math.Cos(radians)) - 8;
                            float textY = CENTER_Y - (float)((arcRadius - markerLength - 15) * Math.Sin(radians)) - 6;
                            g.DrawString(text, font, Brushes.White, textX, textY);
                        }
                    }
                }

                // Draw small triangle at top center (0 degrees)
                float[] triangleXPoints = {
        CENTER_X,
        CENTER_X - 8,
        CENTER_X + 8
    };
                float[] triangleYPoints = {
        CENTER_Y - arcRadius + 2,
        CENTER_Y - arcRadius + 15,
        CENTER_Y - arcRadius + 15
    };
                g.FillPolygon(Brushes.White, new PointF[] {
        new PointF(triangleXPoints[0], triangleYPoints[0]),
        new PointF(triangleXPoints[1], triangleYPoints[1]),
        new PointF(triangleXPoints[2], triangleYPoints[2])
    });
            }

            // Draw roll indicator
            //float rollRadians = (-roll + 90) * (float)Math.PI / 180; // Offset by 90 to center at top
            //using (var pen = new Pen(Color.Yellow, 3))
            //{
            //    int indicatorRadius = RADIUS - 20;
            //    float x = CENTER_X + (float)(indicatorRadius * Math.Cos(rollRadians));
            //    float y = CENTER_Y + (float)(indicatorRadius * Math.Sin(rollRadians));
            //    g.DrawLine(pen, CENTER_X, CENTER_Y - indicatorRadius, x, y);
            //}

            //Draw roll indicator
            //float rollRadians = -roll * (float)Math.PI / 180;
            //using (var pen = new Pen(Color.Yellow, 3))
            //{
            //    float x = CENTER_X + (float)(80 * Math.Sin(rollRadians));
            //    float y = CENTER_Y - (float)(80 * Math.Cos(rollRadians));
            //    g.DrawLine(pen, CENTER_X, CENTER_Y - 85, x, y);
            //}

            // Draw pitch arc at the top
            //using (var pen = new Pen(Color.White, 2))
            //{
            //    // Draw the main arc
            //    g.DrawArc(pen, CENTER_X - 30, CENTER_Y - RADIUS - 30, 60, 60, 0, 180);

            //    // Draw pitch markers on the arc
            //    for (int angle = -90; angle <= 90; angle += 30)
            //    {
            //        double radians = (angle + 90) * Math.PI / 180;
            //        float x1 = CENTER_X + (float)(25 * Math.Cos(radians));
            //        float y1 = CENTER_Y - RADIUS - 30 + (float)(25 * Math.Sin(radians));
            //        float x2 = CENTER_X + (float)(35 * Math.Cos(radians));
            //        float y2 = CENTER_Y - RADIUS - 30 + (float)(35 * Math.Sin(radians));
            //        g.DrawLine(pen, x1, y1, x2, y2);
            //    }
            //}

            //Draw pitch indicator on the arc
            //float pitchArcAngle = (pitch + 90) * (float)Math.PI / 180;
            //using (var pen = new Pen(Color.Yellow, 3))
            //{
            //    float x = CENTER_X + (float)(30 * Math.Cos(pitchArcAngle));
            //    float y = CENTER_Y - RADIUS - 30 + (float)(30 * Math.Sin(pitchArcAngle));
            //    g.DrawLine(pen, CENTER_X, CENTER_Y - RADIUS - 30, x, y);
            //}

            // Draw pitch and roll values
            using (var font = new Font("Arial", 10))
                {
                    g.DrawString($"Pitch: {pitch:F1}°", font, Brushes.White, 10, 10);
                    g.DrawString($"Roll: {roll:F1}°", font, Brushes.White, 10, 30);
                }

            }
        }
    }
