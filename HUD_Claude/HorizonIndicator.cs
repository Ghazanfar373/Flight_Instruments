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
    public partial class HorizonIndicator : UserControl
{
    private float pitch = 0;
    private float roll = 0;
    //private Timer updateTimer;
    private int radius;
    private Point center;
    private const float PITCH_SCALING = 4;

    public HorizonIndicator()
    {
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;
        this.MinimumSize = new Size(200, 200);

        //updateTimer = new Timer();
        //updateTimer.Interval = 50;
        //updateTimer.Tick += UpdateTimer_Tick;
        //updateTimer.Start();

        //this.KeyPreview = true;
        //this.KeyDown += HorizonIndicator_KeyDown;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        // Calculate new radius and center based on control size
        radius = Math.Min(Width, Height) / 2 - 20;
        center = new Point(Width / 2, Height / 2);
        Invalidate();
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
        Invalidate();
    }



        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Black);

            // Save state
            var state = g.Save();

            // Move to center
            g.TranslateTransform(center.X, center.Y);

            // Apply roll rotation
            g.RotateTransform(-roll);

            // Calculate pitch offset
            float pitchOffset = pitch * PITCH_SCALING;

            // Create clip region for horizon mask
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddEllipse(-radius, -radius, radius * 2, radius * 2);
                g.SetClip(path);
            }

            // Draw sky and ground
            using (var skyBrush = new SolidBrush(Color.SkyBlue))
            using (var groundBrush = new SolidBrush(Color.SaddleBrown))
            {
                // Scale rectangle size based on radius
                int rectSize = radius * 3;
                g.FillRectangle(skyBrush, -rectSize, -rectSize - pitchOffset, rectSize * 2, rectSize * 2);
                g.FillRectangle(groundBrush, -rectSize, -pitchOffset, rectSize * 2, rectSize * 2);
            }

            // Draw pitch lines
            using (var pen = new Pen(Color.Wheat, radius / 75f))
            {
                // Scale line lengths based on radius
                float longLine = radius * 0.4f;
                float shortLine = radius * 0.2f;

                // Draw horizon line
                g.DrawLine(pen, -radius, -pitchOffset, radius, -pitchOffset);

                // Replace the pitch line font drawing section with:
                // Draw pitch reference lines
                for (int i = -90; i <= 90; i += 10)
                {
                    if (i == 0) continue;

                    float y = -pitchOffset - (i * PITCH_SCALING);
                    if (y > -radius && y < radius)
                    {
                        float lineLength = (i % 30 == 0) ? longLine : shortLine;
                        g.DrawLine(pen, -lineLength, y, lineLength, y);

                        // Improved font scaling for pitch numbers
                        using (var font = new Font("Arial", Math.Max(radius / 25f, 6f), FontStyle.Regular))
                        {
                            string text = Math.Abs(i).ToString();
                            float textWidth = g.MeasureString(text, font).Width;
                            g.DrawString(text, font, Brushes.Wheat, -lineLength - textWidth - 5, y - font.Height / 2);
                            g.DrawString(text, font, Brushes.Wheat, lineLength + 5, y - font.Height / 2);
                        }
                    }
                }
            }

            // Restore graphics state
            g.Restore(state);

            // Draw fixed elements
            using (var pen = new Pen(Color.Wheat, radius / 75f))
            {
                g.DrawEllipse(pen, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }

            //// Draw roll indicator arc and marks
            //using (var pen = new Pen(Color.Wheat, 2))
            //{
            //    // Draw roll arc inside the horizon at the top
            //    int arcRadius = radius - 20; // Slightly smaller than main horizon circle
            //    g.DrawArc(pen, center.X - arcRadius, center.Y - arcRadius, arcRadius * 2, arcRadius * 2, 210, 120);

            //    // Draw roll markers
            //    for (int angle = -60; angle <= 60; angle += 30)
            //    {
            //        double radians = (angle + 90) * Math.PI / 180; // Offset by 90 to center at top
            //        float markerLength = 10;
            //        float x1 = center.X + (float)((arcRadius - markerLength) * Math.Cos(radians));
            //        float y1 = center.Y + (float)((arcRadius - markerLength) * Math.Sin(radians));
            //        float x2 = center.X + (float)(arcRadius * Math.Cos(radians));
            //        float y2 = center.Y + (float)(arcRadius * Math.Sin(radians));
            //        g.DrawLine(pen, x1, y1, x2, y2);
            //    }
            //}
            // Draw roll indicator arc and marks
            using (var pen = new Pen(Color.Wheat, 2))
            {
                // Draw roll arc inside the horizon at the top
                int arcRadius = radius - 5; // Slightly smaller than main horizon circle
                g.DrawArc(pen, center.X - arcRadius, center.Y - arcRadius, arcRadius * 2, arcRadius * 2, 210, 120);

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
                    float x1 = center.X - (float)((arcRadius - markerLength) * Math.Cos(radians));
                    float y1 = center.Y - (float)((arcRadius - markerLength) * Math.Sin(radians));
                    float x2 = center.X - (float)(arcRadius * Math.Cos(radians));
                    float y2 = center.Y - (float)(arcRadius * Math.Sin(radians));
                    g.DrawLine(pen, x1, y1, x2, y2);

                    // Add labels for 30-degree markers
                    if (angle % 30 == 0 && angle != 0)
                    {
                        using (var font = new Font("Arial", 8))
                        {
                            string text = Math.Abs(angle).ToString();
                            float textX = center.X - (float)((arcRadius - markerLength - 15) * Math.Cos(radians)) - 8;
                            float textY = center.Y - (float)((arcRadius - markerLength - 15) * Math.Sin(radians)) - 6;
                            g.DrawString(text, font, Brushes.Wheat, textX, textY);
                        }
                    }
                }

                // Draw center triangle
                float triangleSize = radius * 0.05f;
                float[] triangleXPoints = {
                center.X,
                center.X - triangleSize,
                center.X + triangleSize
            };
                float[] triangleYPoints = {
                center.Y - arcRadius + 2,
                center.Y - arcRadius + triangleSize * 2,
                center.Y - arcRadius + triangleSize * 2
            };
                g.FillPolygon(Brushes.Red, new PointF[] {
                new PointF(triangleXPoints[0], triangleYPoints[0]),
                new PointF(triangleXPoints[1], triangleYPoints[1]),
                new PointF(triangleXPoints[2], triangleYPoints[2])
            });
            }

            // Draw fixed aircraft reference
            using (var pen = new Pen(Color.Yellow, radius / 50f))
            {
                float refSize = radius * 0.3f;
                g.DrawLine(pen, center.X - refSize, center.Y, center.X - refSize * 0.3f, center.Y);
                g.DrawLine(pen, center.X + refSize * 0.3f, center.Y, center.X + refSize, center.Y);
                g.DrawLine(pen, center.X, center.Y - refSize * 0.3f, center.X, center.Y + refSize * 0.3f);
            }

            // Replace the pitch/roll value display section with:
            // Draw values with improved scaled font
            using (var font = new Font("Arial", Math.Max(radius / 20f, 8f), FontStyle.Regular))
            {
                string pitchText = $"Pitch: {pitch:F1}°";
                string rollText = $"Roll: {roll:F1}°";
                float padding = radius / 30f;

                g.DrawString(pitchText, font, Brushes.Wheat, padding, padding);
                g.DrawString(rollText, font, Brushes.Wheat, padding+150f, padding );
            }
        }

        // Optional: Add properties for pitch and roll if you need to access them from outside + font.Height * 1.2f
    [Browsable(false)]
    public float Pitch
    {
        get { return pitch; }
        set 
        { 
            pitch = Math.Max(Math.Min(value, 90), -90);
            Invalidate();
        }
    }

    [Browsable(false)]
    public float Roll
    {
        get { return roll; }
        set 
        { 
            roll = Math.Max(Math.Min(value, 180), -180);
            Invalidate();
        }
    }
}
}
