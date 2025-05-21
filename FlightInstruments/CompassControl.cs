using System;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HUD_Claude

{
    [ToolboxItem(true)]
    public partial class CompassControl : Control
    {   

          
        private float _heading = 0f;
        private Timer _animationTimer;
        private float _targetHeading = 0f;
        private const float ANIMATION_STEP = 5f;
        private const float PLANE_SIZE = 40f;

        [Description("Current heading of the compass in degrees")]
        public float Heading
        {
            get => _heading;
            set
            {
                _targetHeading = value % 360;
                if (_targetHeading < 0) _targetHeading += 360;
                StartAnimation();
            }
        }

        public CompassControl()
        {
            SetStyle(ControlStyles.DoubleBuffer |
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.ResizeRedraw,
                    true);

            _animationTimer = new Timer();
            _animationTimer.Interval = 50;
            _animationTimer.Tick += AnimationTimer_Tick;
            Size = new Size(200, 200);
        }

        private void StartAnimation()
        {
            if (!_animationTimer.Enabled)
                _animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            float diff = _targetHeading - _heading;

            if (diff > 180) diff -= 360;
            if (diff < -180) diff += 360;

            if (Math.Abs(diff) < ANIMATION_STEP)
            {
                _heading = _targetHeading;
                _animationTimer.Stop();
            }
            else
            {
                _heading += Math.Sign(diff) * ANIMATION_STEP;
                if (_heading >= 360) _heading -= 360;
                if (_heading < 0) _heading += 360;
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int centerX = Width / 2;
            int centerY = Height / 2;
            float radius = Math.Min(Width, Height) / 2 - 10;

            // Save original transform for compass rotation
            Matrix originalTransform = g.Transform;

            // Apply rotation transform for compass dial
            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(-_heading); // Note the negative heading to rotate the dial correctly
            g.TranslateTransform(-centerX, -centerY);

            // Draw rotating compass elements
            // Draw compass circle
            using (Pen circlePen = new Pen(Color.Wheat, 2))
            {
                g.DrawEllipse(circlePen, centerX - radius, centerY - radius, radius * 2, radius * 2);
            }

            // Draw degree markers
            using (Pen markerPen = new Pen(Color.Wheat, 1))
            using (Pen majorMarkerPen = new Pen(Color.Wheat, 2))
            using (Font degreeFont = new Font("Arial", 8))
            {
                for (int degree = 0; degree < 360; degree += 5)
                {
                    float angle = degree * (float)Math.PI / 180;
                    bool isMajor = degree % 30 == 0;
                    float markerLength = isMajor ? 15 : 10;

                    PointF start = new PointF(
                        centerX + (radius - markerLength) * (float)Math.Sin(angle),
                        centerY - (radius - markerLength) * (float)Math.Cos(angle)
                    );
                    PointF end = new PointF(
                        centerX + radius * (float)Math.Sin(angle),
                        centerY - radius * (float)Math.Cos(angle)
                    );

                    g.DrawLine(isMajor ? majorMarkerPen : markerPen, start, end);

                    if (isMajor)
                    {
                        string degreeText = degree.ToString();
                        SizeF textSize = g.MeasureString(degreeText, degreeFont);
                        PointF textPoint = new PointF(
                            centerX + (radius - 30) * (float)Math.Sin(angle) - textSize.Width / 2,
                            centerY - (radius - 30) * (float)Math.Cos(angle) - textSize.Height / 2
                        );
                        g.DrawString(degreeText, degreeFont, Brushes.Wheat, textPoint);
                    }
                }
            }

            // Draw cardinal points
            using (Font cardinalFont = new Font("Arial", 12, FontStyle.Bold))
            {
                string[] cardinals = { "N", "E", "S", "W" };
                float[] angles = { 0, 90, 180, 270 };

                for (int i = 0; i < 4; i++)
                {
                    float angle = angles[i] * (float)Math.PI / 180;
                    PointF textPoint = new PointF(
                        centerX + (radius - 45) * (float)Math.Sin(angle) - 8,
                        centerY - (radius - 45) * (float)Math.Cos(angle) - 8
                    );
                    g.DrawString(cardinals[i], cardinalFont, Brushes.Wheat, textPoint);
                }
            }

            // Restore original transform
            g.Transform = originalTransform;
            //Draw Responsive airplane in center of compass
            float airplaneLength = radius * 0.55F;
            float airplaneWidth = radius * 0.28F;
            PointF[] airplanePoints = new PointF[]
                {
                new PointF(centerX, centerY - airplaneLength), // Nose
                new PointF(centerX-airplaneLength/2, centerY + airplaneLength * 0.25F),
                new PointF(centerX , centerY + airplaneLength * 0.10F), // Left wing
                new PointF(centerX + airplaneLength / 2, centerY + airplaneLength * 0.25F),
                new PointF(centerX, centerY - airplaneLength*0.45f) // top fold midway

                };



            
            

            // Draw the paper airplane
            g.FillPolygon(Brushes.Red, airplanePoints);
            using (Pen outlinePen = new Pen(Color.DarkRed, 1))
            {
                g.DrawPolygon(outlinePen, airplanePoints);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
