using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace HUD_Claude

{
   
        [ToolboxItem(true)]
        [ToolboxBitmap(typeof(CompassControl), "CompassControl.bmp")]
        [Description("Displays an aircraft compass indicator")]
        public partial class CompassControl : Control
        {
            private const float RADIUS = 100f;
            private const int TICK_COUNT = 36; // One tick every 10 degrees
            private const float TICK_LENGTH = 10f;
            private const float HEADING_MARK_LENGTH = 20f;
            private Timer animationTimer;
            private float targetHeading;
            private float currentHeading;

            [Description("Current heading in degrees (0-360)")]
            [Category("Appearance")]
            [DefaultValue(0f)]
            public float Heading
            {
                get { return targetHeading; }
                set
                {
                    targetHeading = value % 360;
                    if (targetHeading < 0) targetHeading += 360;

                    if (!DesignMode && EnableAnimation)
                    {
                        animationTimer.Start();
                    }
                    else
                    {
                        currentHeading = targetHeading;
                        Invalidate();
                    }
                }
            }

            [Description("Enable smooth heading transitions")]
            [Category("Behavior")]
            [DefaultValue(true)]
            public bool EnableAnimation { get; set; } = true;

            public CompassControl()
            {
                SetStyle(
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.ResizeRedraw, true);

                BackColor = Color.Black;
                ForeColor = Color.White;

                // Setup animation timer
                animationTimer = new Timer();
                animationTimer.Interval = 16; // ~60fps
                animationTimer.Tick += AnimationTimer_Tick;
            }

            private void AnimationTimer_Tick(object sender, EventArgs e)
            {
                float diff = targetHeading - currentHeading;

                // Handle wraparound (e.g., 350° to 10°)
                if (diff > 180) diff -= 360;
                if (diff < -180) diff += 360;

                // Smooth animation
                if (Math.Abs(diff) < 0.1f)
                {
                    currentHeading = targetHeading;
                    animationTimer.Stop();
                }
                else
                {
                    currentHeading += diff * 0.1f;
                    if (currentHeading >= 360) currentHeading -= 360;
                    if (currentHeading < 0) currentHeading += 360;
                }

                Invalidate();
            }

            private string GetCardinalPoint(float angle)
            {
                if (angle == 0) return "N";
                if (angle == 90) return "E";
                if (angle == 180) return "S";
                if (angle == 270) return "W";
                if (angle % 30 == 0) return angle.ToString();
                return "";
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Calculate center and scale
                float scale = Math.Min(Width, Height) / (2 * (RADIUS + HEADING_MARK_LENGTH + 20));
                PointF center = new PointF(Width / 2f, Height / 2f);

                using (Pen whitePen = new Pen(ForeColor, 2))
                {
                    // Draw outer circle
                    e.Graphics.DrawEllipse(whitePen,
                        center.X - RADIUS * scale,
                        center.Y - RADIUS * scale,
                        RADIUS * 2 * scale,
                        RADIUS * 2 * scale);

                    // Draw ticks and labels
                    for (int i = 0; i < TICK_COUNT; i++)
                    {
                        float angle = i * (360f / TICK_COUNT);
                        float radians = (angle - currentHeading) * (float)Math.PI / 180f;

                        // Calculate tick positions
                        float tickLength = (i % 9 == 0) ? HEADING_MARK_LENGTH : TICK_LENGTH;
                        PointF start = new PointF(
                            center.X + (RADIUS * scale) * (float)Math.Sin(radians),
                            center.Y - (RADIUS * scale) * (float)Math.Cos(radians)
                        );
                        PointF end = new PointF(
                            center.X + ((RADIUS - tickLength) * scale) * (float)Math.Sin(radians),
                            center.Y - ((RADIUS - tickLength) * scale) * (float)Math.Cos(radians)
                        );

                        // Draw tick
                        e.Graphics.DrawLine(whitePen, start, end);

                        // Draw cardinal points and degree labels
                        if (i % 9 == 0)
                        {
                            string text = GetCardinalPoint(angle);

                            if (!string.IsNullOrEmpty(text))
                            {
                                using (Font font = new Font("Arial", 12 * scale))
                                {
                                    SizeF textSize = e.Graphics.MeasureString(text, font);
                                    PointF textPos = new PointF(
                                        center.X + ((RADIUS - HEADING_MARK_LENGTH - 15) * scale) * (float)Math.Sin(radians) - textSize.Width / 2,
                                        center.Y - ((RADIUS - HEADING_MARK_LENGTH - 15) * scale) * (float)Math.Cos(radians) - textSize.Height / 2
                                    );
                                    e.Graphics.DrawString(text, font, new SolidBrush(ForeColor), textPos);
                                }
                            }
                        }
                    }

                    // Draw aircraft reference marker (triangle)
                    float triangleSize = 10 * scale;
                    PointF[] trianglePoints = new PointF[]
                    {
                    new PointF(center.X, center.Y - (RADIUS + 5) * scale),
                    new PointF(center.X - triangleSize, center.Y - (RADIUS - 5) * scale),
                    new PointF(center.X + triangleSize, center.Y - (RADIUS - 5) * scale)
                    };

                    using (Brush yellowBrush = new SolidBrush(Color.Yellow))
                    using (Pen yellowPen = new Pen(Color.Yellow, 2))
                    {
                        e.Graphics.FillPolygon(yellowBrush, trianglePoints);
                        e.Graphics.DrawPolygon(yellowPen, trianglePoints);
                    }
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    animationTimer?.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    
}
