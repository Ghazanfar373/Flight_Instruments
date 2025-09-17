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
    public partial class APStatusControl : UserControl
    {
        // New status properties
        private bool isArmed = false;
        private string autopilotMode = "MANUAL";
        // Status display scaling
        private int radius;
        private float statusFontScale => Math.Max(radius / 15f, 6f);
        private float statusPadding => Math.Max(radius / 25f, 2f);







        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Use original working graphics settings
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Draw status displays first (before clipping)
            DrawStatusDisplays(g);

            // Save state
            var state = g.Save();

            // Move to center
            // g.TranslateTransform(center.X, center.Y);

        }

        private void DrawStatusDisplays(Graphics g)
        {
            using (var font = new Font("Arial", statusFontScale, FontStyle.Regular))
            {
                // ARM Status - Upper Left
                string armText = isArmed ? "ARMED" : "DISARMED";
                Color armColor = isArmed ? Color.Red : Color.Yellow;
                Color armBgColor = isArmed ? Color.FromArgb(120, 255, 0, 0) : Color.FromArgb(120, 255, 255, 0);

                DrawStatusBox(g, armText, font, armColor, armBgColor, statusPadding, statusPadding, true);

                // Autopilot Mode - Upper Right
                Color modeColor = GetModeColor(autopilotMode);
                Color modeBgColor = Color.FromArgb(120, modeColor.R, modeColor.G, modeColor.B);

                SizeF modeSize = g.MeasureString(autopilotMode, font);
                float rightX = Width - modeSize.Width - statusPadding * 3;

                DrawStatusBox(g, autopilotMode, font, modeColor, modeBgColor, rightX, statusPadding, false);
            }
        }

        private void DrawStatusBox(Graphics g, string text, Font font, Color textColor, Color bgColor, float x, float y, bool leftAlign)
        {
            //SizeF textSize = g.MeasureString(text, font);
            //float boxWidth = textSize.Width + statusPadding * 1;
            //float boxHeight = textSize.Height + statusPadding;

            //// Adjust position if right-aligned
            //if (!leftAlign)
            //{
            //    x = Width - boxWidth - statusPadding;
            //}

            //// Draw background box
            //using (var bgBrush = new SolidBrush(bgColor))
            //using (var borderPen = new Pen(textColor, 1f))
            //{
            //    RectangleF boxRect = new RectangleF(x, y, boxWidth, boxHeight);
            //    g.FillRectangle(bgBrush, boxRect);
            //    g.DrawRectangle(borderPen, x, y, boxWidth, boxHeight);
            //}

            // Draw text with outline for better visibility
            float textX = x + statusPadding;
            float textY = y + statusPadding / 2;

            DrawOutlinedText(g, text, font, new SolidBrush(textColor), Brushes.Black, textX, textY, 1);
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
        private Color GetModeColor(string mode)
        {
            switch (mode.ToUpper())
            {
                case "MANUAL":
                    return Color.White;
                case "STABILIZE":
                    return Color.Cyan;
                case "ALT_HOLD":
                    return Color.LightBlue;
                case "AUTO":
                    return Color.Green;
                case "GUIDED":
                    return Color.Lime;
                case "LOITER":
                    return Color.Orange;
                case "RTL":
                    return Color.Magenta;
                case "LAND":
                    return Color.Yellow;
                default:
                    return Color.Gray;
            }
        }
        // New status properties
        [Browsable(true)]
        [Category("Status")]
        [Description("Aircraft armed status")]
        public bool IsArmed
        {
            get { return isArmed; }
            set
            {
                if (isArmed != value)
                {
                    isArmed = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Status")]
        [Description("Current autopilot flight mode")]
        public string AutopilotMode
        {
            get { return autopilotMode; }
            set
            {
                if (autopilotMode != value)
                {
                    autopilotMode = value ?? "MANUAL";
                    Invalidate();
                }
            }
        }
    

}
}
