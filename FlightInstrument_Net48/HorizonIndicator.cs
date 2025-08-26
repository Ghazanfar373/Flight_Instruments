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
        private int radius;
        private Point center;
        // Mission Planner standard pitch scaling
        private float PITCH_SCALING => this.Height / 100f;

        // Responsive scaling factors optimized for 120x120 size
        private float penWidthScale => Math.Max(radius / 80f, 1f);
        private float fontSizeScale => Math.Max(radius / 20f, 6f);
        private float triangleSizeScale => radius * 0.08f;
        private float refSizeScale => radius * 0.35f;
        private float paddingScale => Math.Max(radius / 25f, 2f);

        public HorizonIndicator()
        {
            SetStyle(ControlStyles.DoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.ResizeRedraw |
                        ControlStyles.SupportsTransparentBackColor,
                        true);

            // Set transparent background
            BackColor = Color.Transparent;
            Size = new Size(120, 120);

            // Enable focus for keyboard input
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            // Calculate new radius and center based on control size optimized for smaller size
            int minDimension = Math.Min(Width, Height);
            radius = Math.Max(minDimension / 2 - 8, 40); // Reduced padding and minimum radius for 120x120
            center = new Point(Width / 2, Height / 2);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HorizonIndicator_KeyDown(this, e);
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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Clear the background properly for transparency
            e.Graphics.Clear(Color.Transparent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Use original working graphics settings
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Save state
            var state = g.Save();

            // Move to center
            g.TranslateTransform(center.X, center.Y);

            // Apply roll rotation
            g.RotateTransform(-roll);

            // Calculate pitch offset
            float pitchOffset = -pitch * PITCH_SCALING;

            // Create clip region for horizon mask
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddEllipse(-radius, -radius, radius * 2, radius * 2);
                g.SetClip(path);
            }

            // Draw sky and ground - back to original opacity
            using (var skyBrush = new SolidBrush(Color.CornflowerBlue))
            using (var groundBrush = new SolidBrush(Color.Green))
            {
                int rectSize = radius * 3;
                g.FillRectangle(skyBrush, -rectSize, -rectSize - pitchOffset, rectSize * 2, rectSize * 2);
                g.FillRectangle(groundBrush, -rectSize, -pitchOffset, rectSize * 2, rectSize * 2);
            }

            // Draw pitch lines with responsive scaling
            using (var pen = new Pen(Color.White, penWidthScale))
            {
                float longLine = radius * 0.4f;
                float shortLine = radius * 0.2f;

                //// Draw horizon line (thicker) at the actual horizon position
                //using (var horizonPen = new Pen(Color.White, penWidthScale * 2))
                //{
                //    g.DrawLine(horizonPen, -radius, pitchOffset, radius, pitchOffset);
                //}

                // Draw pitch reference lines
                for (int i = -90; i <= 90; i += 10)
                {
                    if (i == 0) continue;

                    float y = -pitchOffset - (i * PITCH_SCALING);
                    if (y > -radius && y < radius)
                    {
                        float lineLength = (i % 30 == 0) ? longLine : shortLine;
                        g.DrawLine(pen, -lineLength, y, lineLength, y);

                        // Responsive font scaling for pitch numbers
                        using (var font = new Font("Arial", fontSizeScale, FontStyle.Regular))
                        {
                            string text = Math.Abs(i).ToString();
                            var textSize = g.MeasureString(text, font);
                            float textOffset = lineLength + paddingScale;

                            // Draw simple white text
                            g.DrawString(text, font, Brushes.White, -textOffset - textSize.Width, y - textSize.Height / 2);
                            g.DrawString(text, font, Brushes.White, textOffset, y - textSize.Height / 2);
                        }
                    }
                }
            }

            // Restore graphics state
            g.Restore(state);

            // Draw outer circle with responsive thickness
            using (var pen = new Pen(Color.White, penWidthScale))
            {
                g.DrawEllipse(pen, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }

            // Draw roll scale and indicator with responsive scaling
            DrawRollScale(g);

            // Draw fixed aircraft reference with responsive scaling
            using (var pen = new Pen(Color.Red, Math.Max(penWidthScale, 2f)))
            {
                float refSize = refSizeScale;
                g.DrawLine(pen, center.X - refSize, center.Y, center.X - refSize * 0.3f, center.Y);
                g.DrawLine(pen, center.X + refSize * 0.3f, center.Y, center.X + refSize, center.Y);
                g.DrawLine(pen, center.X, center.Y - refSize * 0.3f, center.X, center.Y + refSize * 0.3f);
            }

            // Draw values with responsive font and positioning
            DrawValueDisplay(g);
        }

        private void DrawRollScale(Graphics g)
        {
            using (var pen = new Pen(Color.White, penWidthScale))
            {
                int arcRadius = radius - (int)(radius * 0.1f);

                // Draw the fixed markers on the arc
                for (int angle = -60; angle <= 60; angle += 10)
                {
                    double radians = (-angle + 90) * Math.PI / 180;
                    float markerLength;

                    if (angle == 0)
                        markerLength = radius * 0.1f;
                    else if (angle % 30 == 0)
                        markerLength = radius * 0.08f;
                    else
                        markerLength = radius * 0.05f;

                    float x1 = center.X + (float)((arcRadius - markerLength) * Math.Cos(radians));
                    float y1 = center.Y - (float)((arcRadius - markerLength) * Math.Sin(radians));
                    float x2 = center.X + (float)(arcRadius * Math.Cos(radians));
                    float y2 = center.Y - (float)(arcRadius * Math.Sin(radians));
                    g.DrawLine(pen, x1, y1, x2, y2);

                    // Draw angle labels for major marks
                    //if (angle % 30 == 0 && angle != 0)
                    //{
                    //    using (var font = new Font("Arial", fontSizeScale * 0.8f, FontStyle.Regular))
                    //    {
                    //        string text = Math.Abs(angle).ToString();
                    //        var textSize = g.MeasureString(text, font);
                    //        float textX = center.X + (float)((arcRadius - markerLength - radius * 0.15f) * Math.Cos(radians)) - textSize.Width / 2;
                    //        float textY = center.Y - (float)((arcRadius - markerLength - radius * 0.15f) * Math.Sin(radians)) - textSize.Height / 2;

                    //        g.DrawString(text, font, Brushes.White, textX, textY);
                    //    }
                    //}
                }

                // Draw the moving roll indicator (triangle)
                DrawRollIndicator(g, arcRadius);

                // Draw center reference triangle
                DrawCenterTriangle(g, arcRadius);
            }
        }

        private void DrawRollIndicator(Graphics g, int arcRadius)
        {
            float triangleSize = triangleSizeScale;
            double rollRadians = (roll + 90) * Math.PI / 180;

            float triangleCenterX = center.X + (float)(arcRadius * Math.Cos(rollRadians));
            float triangleCenterY = center.Y - (float)(arcRadius * Math.Sin(rollRadians));

            // Calculate triangle points
            PointF[] trianglePoints = new PointF[3];
            double tipAngle = rollRadians + Math.PI;

            trianglePoints[0] = new PointF(
                triangleCenterX + (float)(triangleSize * Math.Cos(tipAngle)),
                triangleCenterY - (float)(triangleSize * Math.Sin(tipAngle))
            );

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

            // Draw triangle with outline for better visibility
            using (var brush = new SolidBrush(Color.Yellow))
            using (var pen = new Pen(Color.YellowGreen, 1f))
            {
                g.FillPolygon(brush, trianglePoints);
                g.DrawPolygon(pen, trianglePoints);
            }
        }

        private void DrawCenterTriangle(Graphics g, int arcRadius)
        {
            float triangleSize = triangleSizeScale;
            PointF[] centerTriangle = {
                new PointF(center.X, center.Y - arcRadius + 2),
                new PointF(center.X - triangleSize, center.Y - arcRadius + triangleSize * 2),
                new PointF(center.X + triangleSize, center.Y - arcRadius + triangleSize * 2)
            };

            using (var brush = new SolidBrush(Color.Red))
            using (var pen = new Pen(Color.Red, 1f))
            {
                g.FillPolygon(brush, centerTriangle);
                g.DrawPolygon(pen, centerTriangle);
            }
        }

        private void DrawValueDisplay(Graphics g)
        {
            // Value display removed - clean instrument look
        }

        private void DrawOutlinedText(Graphics g, string text, Font font, Brush textBrush,
            Brush outlineBrush, float x, float y, int outlineWidth)
        {
            // Draw outline
            for (int dx = -outlineWidth; dx <= outlineWidth; dx++)
            {
                for (int dy = -outlineWidth; dy <= outlineWidth; dy++)
                {
                    if (dx != 0 || dy != 0)
                        g.DrawString(text, font, outlineBrush, x + dx, y + dy);
                }
            }
            // Draw main text
            g.DrawString(text, font, textBrush, x, y);
        }

        [Browsable(true)]
        [Category("Attitude")]
        [Description("Pitch angle in degrees (-90 to +90)")]
        public float Pitch
        {
            get { return pitch; }
            set
            {
                float newValue = Math.Max(Math.Min(value, 90), -90);
                if (Math.Abs(pitch - newValue) > 0.01f) // Only invalidate if changed
                {
                    pitch = newValue;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Attitude")]
        [Description("Roll angle in degrees (-180 to +180)")]
        public float Roll
        {
            get { return roll; }
            set
            {
                float newValue = Math.Max(Math.Min(value, 180), -180);
                if (Math.Abs(roll - newValue) > 0.01f) // Only invalidate if changed
                {
                    roll = newValue;
                    Invalidate();
                }
            }
        }
    }
}