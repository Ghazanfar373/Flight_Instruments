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
    public partial class HorizonIndicator2 : UserControl
    {
        private float pitch = 0;
        private float roll = 0;
        private int radius;
        private Point center;

        // Mission Planner compatible pitch scaling - degrees to pixels
        private float PITCH_SCALING => radius / 45f; // 45 degrees spans the radius

        // Optimized scaling factors specifically for 120x120 base size
        private float penWidthScale => Math.Max(radius / 50f, 1f); // Adjusted for 120x120
        private float fontSizeScale => Math.Max(radius / 8f, 8f); // Larger font for better visibility
        private float triangleSizeScale => radius * 0.15f; // Larger triangles
        private float refSizeScale => radius * 0.5f; // Larger aircraft reference
        private float paddingScale => Math.Max(radius / 15f, 4f); // Better text spacing
        private float horizonPenScale => Math.Max(radius / 30f, 2f); // Thicker horizon line
        private float rollScaleOffset => Math.Max(radius * 0.12f, 8f); // Closer roll scale for 120x120
        private float markerLengthScale => radius * 0.2f; // Appropriate marker size

        public HorizonIndicator2()
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
            // Calculate radius optimized for 120x120 size
            int minDimension = Math.Min(Width, Height);

            // For 120x120, this gives radius = 52 (120/2 - 8 = 52)
            radius = Math.Max(minDimension / 2 - 8, 25);
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

            // Use high-quality rendering
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Create clip region for the circular instrument
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddEllipse(center.X - radius, center.Y - radius, radius * 2, radius * 2);
                g.SetClip(path);
            }

            // Save state before transformations
            var state = g.Save();

            // Move to center and apply roll rotation
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(-roll); // Mission Planner uses negative roll

            // Calculate pitch offset - FIXED: positive pitch moves horizon UP (nose up shows more ground)
            float pitchOffset = -pitch * PITCH_SCALING;

            // Draw sky and ground - larger rectangles to cover rotated view
            using (var skyBrush = new SolidBrush(Color.FromArgb(68, 142, 215))) // Mission Planner blue
            using (var groundBrush = new SolidBrush(Color.FromArgb(165, 102, 42))) // Mission Planner brown
            {
                int rectSize = radius * 4; // Large enough to cover any rotation
                // Sky above horizon line
                g.FillRectangle(skyBrush, -rectSize, -rectSize, rectSize * 2, rectSize - pitchOffset);
                // Ground below horizon line
                g.FillRectangle(groundBrush, -rectSize, -pitchOffset, rectSize * 2, rectSize * 2);
            }

            // Draw horizon line - the actual horizon
            using (var horizonPen = new Pen(Color.White, horizonPenScale))
            {
                g.DrawLine(horizonPen, -radius * 2, -pitchOffset, radius * 2, -pitchOffset);
            }

            // Draw pitch reference lines with responsive scaling
            using (var pen = new Pen(Color.White, penWidthScale))
            {
                for (int pitchAngle = -90; pitchAngle <= 90; pitchAngle += 10)
                {
                    if (pitchAngle == 0) continue; // Skip horizon line

                    float y = -pitchAngle * PITCH_SCALING + pitchOffset; // FIXED: corrected calculation

                    // Only draw lines that are visible within the instrument
                    if (Math.Abs(y) < radius * 1.8f) // Increased visible range
                    {
                        float lineLength;
                        if (Math.Abs(pitchAngle) % 30 == 0)
                            lineLength = markerLengthScale * 1.2f; // Long lines for major marks
                        else if (Math.Abs(pitchAngle) % 20 == 0)
                            lineLength = markerLengthScale * 0.9f; // Medium lines
                        else
                            lineLength = markerLengthScale * 0.6f; // Short lines for minor marks

                        // Responsive line dash pattern for minor marks
                        if (Math.Abs(pitchAngle) % 30 != 0 && radius < 80)
                        {
                            // Use dashed lines for small sizes to reduce clutter
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        }
                        else
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        }

                        // Draw the pitch line
                        g.DrawLine(pen, -lineLength, y, lineLength, y);

                        // Draw pitch angle text for major marks with optimized positioning
                        if (Math.Abs(pitchAngle) % 30 == 0)
                        {
                            float responsiveFontSize = fontSizeScale * 0.8f; // 8-9px for 120x120
                            using (var font = new Font("Arial", responsiveFontSize, FontStyle.Bold))
                            {
                                string text = Math.Abs(pitchAngle).ToString();
                                var textSize = g.MeasureString(text, font);
                                float textOffset = lineLength + paddingScale * 1.5f; // Extra spacing

                                // Always draw text for 120x120 size - adjusted positioning
                                g.DrawString(text, font, Brushes.White,
                                    -textOffset - textSize.Width, y - textSize.Height / 2);
                                g.DrawString(text, font, Brushes.White,
                                    textOffset, y - textSize.Height / 2);
                            }
                        }
                    }
                }
                // Reset pen style
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            }

            // Restore graphics state
            g.Restore(state);

            // Reset clipping for outer elements
            g.ResetClip();

            // Draw outer circle
            using (var pen = new Pen(Color.White, penWidthScale))
            {
                g.DrawEllipse(pen, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }

            // Draw roll scale and indicators
            DrawRollScale(g);

            // Draw fixed aircraft reference symbol (always in center, not rotated)
            using (var pen = new Pen(Color.Yellow, Math.Max(penWidthScale * 1.5f, 2f)))
            {
                float refSize = refSizeScale;
                float wingThickness = Math.Max(penWidthScale * 2f, 3f);
                float dotSize = Math.Max(radius / 30f, 3f);

                // Horizontal line (wings) - responsive thickness
                using (var wingPen = new Pen(Color.Yellow, wingThickness))
                {
                    g.DrawLine(wingPen, center.X - refSize, center.Y, center.X - refSize * 0.15f, center.Y);
                    g.DrawLine(wingPen, center.X + refSize * 0.15f, center.Y, center.X + refSize, center.Y);
                }

                // Vertical line (fuselage reference)
                g.DrawLine(pen, center.X, center.Y, center.X, center.Y + refSize * 0.4f);

                // Center dot - responsive size
                g.FillEllipse(new SolidBrush(Color.Yellow),
                    center.X - dotSize / 2, center.Y - dotSize / 2, dotSize, dotSize);

                // Add small wing tips for better visibility on larger sizes
                if (radius > 80)
                {
                    float tipSize = refSize * 0.1f;
                    g.DrawLine(pen, center.X - refSize, center.Y, center.X - refSize, center.Y - tipSize);
                    g.DrawLine(pen, center.X + refSize, center.Y, center.X + refSize, center.Y - tipSize);
                }
            }
        }

        private void DrawRollScale(Graphics g)
        {
            using (var pen = new Pen(Color.White, penWidthScale))
            {
                int arcRadius = radius + (int)rollScaleOffset; // Responsive distance from main circle

                // Draw roll scale marks with responsive intervals
                int interval = radius < 60 ? 20 : (radius < 120 ? 15 : 10); // Fewer marks for small sizes
                int maxAngle = radius < 60 ? 40 : 60; // Reduced range for small sizes

                for (int angle = -maxAngle; angle <= maxAngle; angle += interval)
                {
                    // Convert to radians - 0 degrees is at top
                    double radians = (90 - angle) * Math.PI / 180;

                    float markerLength;
                    if (angle == 0)
                        markerLength = markerLengthScale; // Longest for center
                    else if (angle % 30 == 0)
                        markerLength = markerLengthScale * 0.8f; // Long for major marks
                    else
                        markerLength = markerLengthScale * 0.5f; // Short for minor marks

                    // Calculate marker positions
                    float x1 = center.X + (float)(arcRadius * Math.Cos(radians));
                    float y1 = center.Y - (float)(arcRadius * Math.Sin(radians));
                    float x2 = center.X + (float)((arcRadius - markerLength) * Math.Cos(radians));
                    float y2 = center.Y - (float)((arcRadius - markerLength) * Math.Sin(radians));

                    g.DrawLine(pen, x1, y1, x2, y2);

                    // Draw angle labels for major marks - only on larger sizes
                    if (angle % 30 == 0 && angle != 0 && radius > 60)
                    {
                        float labelFontSize = Math.Max(fontSizeScale * 0.6f, 6f);
                        using (var font = new Font("Arial", labelFontSize, FontStyle.Regular))
                        {
                            string text = Math.Abs(angle).ToString();
                            var textSize = g.MeasureString(text, font);

                            float textOffset = markerLength + paddingScale;
                            float textX = center.X + (float)((arcRadius - textOffset) * Math.Cos(radians)) - textSize.Width / 2;
                            float textY = center.Y - (float)((arcRadius - textOffset) * Math.Sin(radians)) - textSize.Height / 2;

                            // Use simple white text
                            g.DrawString(text, font, Brushes.White, textX, textY);
                        }
                    }
                }

                // Draw moving roll indicator triangle
                DrawRollIndicator(g, arcRadius);

                // Draw fixed center reference triangle (pointing down at 0 degrees)
                DrawCenterTriangle(g, arcRadius);
            }
        }

        private void DrawRollIndicator(Graphics g, int arcRadius)
        {
            float triangleSize = triangleSizeScale;

            // Calculate position based on roll angle (0 degrees at top)
            double rollRadians = (90 - roll) * Math.PI / 180;

            float triangleCenterX = center.X + (float)(arcRadius * Math.Cos(rollRadians));
            float triangleCenterY = center.Y - (float)(arcRadius * Math.Sin(rollRadians));

            // Create triangle pointing inward toward center - responsive size
            PointF[] trianglePoints = new PointF[3];

            // Tip points toward center
            double tipAngle = rollRadians + Math.PI;
            trianglePoints[0] = new PointF(
                triangleCenterX + (float)(triangleSize * Math.Cos(tipAngle)),
                triangleCenterY - (float)(triangleSize * Math.Sin(tipAngle))
            );

            // Base points - responsive width
            double baseAngle1 = rollRadians - Math.PI / 2;
            double baseAngle2 = rollRadians + Math.PI / 2;
            float baseWidth = triangleSize * 0.8f;

            trianglePoints[1] = new PointF(
                triangleCenterX + (float)(baseWidth * Math.Cos(baseAngle1)),
                triangleCenterY - (float)(baseWidth * Math.Sin(baseAngle1))
            );

            trianglePoints[2] = new PointF(
                triangleCenterX + (float)(baseWidth * Math.Cos(baseAngle2)),
                triangleCenterY - (float)(baseWidth * Math.Sin(baseAngle2))
            );

            // Draw triangle with responsive outline
            float outlineWidth = Math.Max(penWidthScale, 1f);
            using (var brush = new SolidBrush(Color.Yellow))
            using (var outlinePen = new Pen(Color.Black, outlineWidth))
            {
                g.FillPolygon(brush, trianglePoints);
                g.DrawPolygon(outlinePen, trianglePoints);
            }
        }

        private void DrawCenterTriangle(Graphics g, int arcRadius)
        {
            float triangleSize = triangleSizeScale;

            // Fixed triangle at top center (0 degrees reference) - responsive size
            float tipDistance = triangleSize * 1.2f;
            float baseWidth = triangleSize * 0.9f;

            PointF[] centerTriangle = {
                new PointF(center.X, center.Y - arcRadius + tipDistance), // Tip pointing in
                new PointF(center.X - baseWidth, center.Y - arcRadius), // Left base
                new PointF(center.X + baseWidth, center.Y - arcRadius)  // Right base
            };

            float outlineWidth = Math.Max(penWidthScale, 1f);
            using (var brush = new SolidBrush(Color.Red))
            using (var outlinePen = new Pen(Color.DarkRed, outlineWidth))
            {
                g.FillPolygon(brush, centerTriangle);
                g.DrawPolygon(outlinePen, centerTriangle);
            }
        }

        private void DrawOutlinedText(Graphics g, string text, Font font, Brush textBrush,
            Brush outlineBrush, float x, float y, int outlineWidth)
        {
            // Simple white text only - no outline needed
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
                if (Math.Abs(pitch - newValue) > 0.01f)
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
                if (Math.Abs(roll - newValue) > 0.01f)
                {
                    roll = newValue;
                    Invalidate();
                }
            }
        }
    }
}