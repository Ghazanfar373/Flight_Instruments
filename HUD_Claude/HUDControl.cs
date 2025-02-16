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
    [ToolboxItem(true)]
    public partial class HUDControl : UserControl
    {


        private float pitch = 0; // Degrees, positive is nose up
        private float roll = 0;  // Degrees, positive is right roll
        private const int CENTER_X = 200;
        private const int CENTER_Y = 200;
        private const int RADIUS = 150;
        private const float PITCH_SCALING = 4; // Pixels per degree of pitch

        public HUDControl()
        {


            this.ClientSize = new Size(400, 400);
            this.DoubleBuffered = true;
        }



        [Description("Current Pitch in degrees (0-360)")]
        [Category("Appearance")]
        [DefaultValue(0f)]
        public float Pitch
        {
            get { return pitch; } set { pitch = value;
                this.Invalidate();
            }
        }
        [Description("Current Pitch in degrees (0-360)")]
        [Category("Appearance")]
        [DefaultValue(0f)]
        public float Roll
        {
            get { return roll; }
            set { roll = value;
                this.Invalidate();
            }

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
                g.FillRectangle(skyBrush, -RADIUS, -RADIUS, RADIUS * 2, RADIUS * 2);

                // Fill ground (bottom half)
                g.FillRectangle(groundBrush, -RADIUS, -pitchOffset, RADIUS * 2, RADIUS * 2);
            }

            // Draw pitch lines
            using (var pen = new Pen(Color.Wheat, 2))
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
                            g.DrawString(text, font, Brushes.Wheat, -lineLength - 20, y - 6);
                            g.DrawString(text, font, Brushes.Wheat, lineLength + 5, y - 6);
                        }
                    }
                }
            }

            // Restore graphics state
            g.Restore(state);

            // Draw fixed elements after restoring the state
            // Draw circular mask
            using (var pen = new Pen(Color.Wheat, 3))
            {
                g.DrawEllipse(pen, CENTER_X - RADIUS, CENTER_Y - RADIUS, RADIUS * 2, RADIUS * 2);
            }

            // Draw fixed aircraft reference
            using (var pen = new Pen(Color.Yellow, 5))
            {
                g.DrawLine(pen, CENTER_X - 50, CENTER_Y, CENTER_X - 15, CENTER_Y);
                g.DrawLine(pen, CENTER_X + 15, CENTER_Y, CENTER_X + 50, CENTER_Y);
                g.DrawLine(pen, CENTER_X, CENTER_Y - 15, CENTER_X, CENTER_Y + 15);
            }

            //// Draw roll indicator arc and marks
            //using (var pen = new Pen(Color.Wheat, 2))
            //{
            //    // Draw roll arc inside the horizon at the top
            //    int arcRADIUS = RADIUS - 10; // Slightly smaller than main horizon circle
            //    g.DrawArc(pen, CENTER_X - arcRADIUS, CENTER_Y - arcRADIUS, arcRADIUS * 2, arcRADIUS * 2, 210, 120);

            //    // Draw roll markers
            //    for (int angle = -60; angle <= 60; angle += 30)
            //    {
            //        double radians = (angle + 90) * Math.PI / 180; // Offset by 90 to center at top
            //        float markerLength = 10;
            //        float x1 = CENTER_X + (float)((arcRADIUS - markerLength) * Math.Cos(radians));
            //        float y1 = CENTER_Y + (float)((arcRADIUS - markerLength) * Math.Sin(radians));
            //        float x2 = CENTER_X + (float)(arcRADIUS * Math.Cos(radians));
            //        float y2 = CENTER_Y + (float)(arcRADIUS * Math.Sin(radians));
            //        g.DrawLine(pen, x1, y1, x2, y2);
            //    }
            //}
            // Replace the roll indicator arc and marker drawing section with:
            using (var pen = new Pen(Color.Wheat, RADIUS / 75f))
            {
                int arcRADIUS = RADIUS - RADIUS / 10;

                // Draw the fixed roll arc and markers at the top (changed from 210,120 to 30,120)
                //g.DrawArc(pen, CENTER_X - arcRADIUS, CENTER_Y - arcRADIUS, arcRADIUS * 2, arcRADIUS * 2, 30, 120);

                // Draw the fixed markers on the arc, adjusted angles for top position
                for (int angle = -60; angle <= 60; angle += 10)
                {
                    double radians = (-angle + 90) * Math.PI / 180; // Changed angle calculation for top position
                    float markerLength;

                    if (angle == 0)
                    {
                        markerLength = RADIUS * 0.1f;
                    }
                    else if (angle % 30 == 0)
                    {
                        markerLength = RADIUS * 0.08f;
                    }
                    else
                    {
                        markerLength = RADIUS * 0.05f;
                    }

                    float x1 = CENTER_X + (float)((arcRADIUS - markerLength) * Math.Cos(radians));
                    float y1 = CENTER_Y - (float)((arcRADIUS - markerLength) * Math.Sin(radians));
                    float x2 = CENTER_X + (float)(arcRADIUS * Math.Cos(radians));
                    float y2 = CENTER_Y - (float)(arcRADIUS * Math.Sin(radians));
                    g.DrawLine(pen, x1, y1, x2, y2);

                    if (angle % 30 == 0 && angle != 0)
                    {
                        using (var font = new Font("Arial", Math.Max(RADIUS / 25f, 10f), FontStyle.Regular))
                        {
                            string text = Math.Abs(angle).ToString();
                            float textWidth = g.MeasureString(text, font).Width;
                            float textHeight = font.Height;
                            float textX = CENTER_X + (float)((arcRADIUS - markerLength - RADIUS * 0.15f) * Math.Cos(radians)) - textWidth / 2;
                            float textY = CENTER_Y - (float)((arcRADIUS - markerLength - RADIUS * 0.15f) * Math.Sin(radians)) - textHeight / 2;
                            g.DrawString(text, font, Brushes.Wheat, textX, textY);
                        }
                    }
                }

                // Draw the moving roll indicator (triangle) at the top
                float triangleSize = RADIUS * 0.05f;
                double rollRadians = (roll + 90) * Math.PI / 180; // Adjusted for top position

                float triangleCenterX = CENTER_X + (float)(arcRADIUS * Math.Cos(rollRadians));
                float triangleCenterY = CENTER_Y - (float)(arcRADIUS * Math.Sin(rollRadians));

                // Calculate triangle points
                PointF[] trianglePoints = new PointF[3];

                // Point at the tip of the triangle (pointing towards center)
                double tipAngle = rollRadians + Math.PI; // Point towards center
                trianglePoints[0] = new PointF(
                    triangleCenterX + (float)(triangleSize * Math.Cos(tipAngle)),
                    triangleCenterY - (float)(triangleSize * Math.Sin(tipAngle))
                );

                // Base points of the triangle
                double leftAngle = rollRadians - Math.PI / 2;
                double rightAngle = rollRadians + Math.PI / 2;

                trianglePoints[1] = new PointF(
                    triangleCenterX + (float)(triangleSize * Math.Cos(leftAngle)),
                    triangleCenterY - (float)(triangleSize * Math.Sin(leftAngle))
                );

                trianglePoints[2] = new PointF(
                    triangleCenterX + (float)(triangleSize * Math.Cos(rightAngle)),
                    triangleCenterY - (float)(triangleSize * Math.Sin(rightAngle))
                );

                // Draw the triangle
                using (var brush = new SolidBrush(Color.Yellow))
                {
                    g.FillPolygon(brush, trianglePoints);
                }
            


            // Draw small triangle at top center (0 degrees)
            float[] triangleXPoints = {
        CENTER_X,
        CENTER_X - 8,
        CENTER_X + 8
    };
                float[] triangleYPoints = {
        CENTER_Y - arcRADIUS + 2,
        CENTER_Y - arcRADIUS + 15,
        CENTER_Y - arcRADIUS + 15
    };
                g.FillPolygon(Brushes.Red, new PointF[] {
        new PointF(triangleXPoints[0], triangleYPoints[0]),
        new PointF(triangleXPoints[1], triangleYPoints[1]),
        new PointF(triangleXPoints[2], triangleYPoints[2])
    });

            }
            // Draw roll indicator
            //float rollRadians = (-roll + 90) * (float)Math.PI / 180; // Offset by 90 to center at top
            //using (var pen = new Pen(Color.Yellow, 3))
            //{
            //    int indicatorRADIUS = RADIUS - 20;
            //    float x = CENTER_X + (float)(indicatorRADIUS * Math.Cos(rollRadians));
            //    float y = CENTER_Y + (float)(indicatorRADIUS * Math.Sin(rollRadians));
            //    g.DrawLine(pen, CENTER_X, CENTER_Y - indicatorRADIUS, x, y);
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
            //using (var pen = new Pen(Color.Wheat, 2))
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
                g.DrawString($"Pitch: {pitch:F1}°", font, Brushes.Wheat, 10, 10);
                g.DrawString($"Roll: {roll:F1}°", font, Brushes.Wheat, 10, 30);
            }

        }
    }
}
