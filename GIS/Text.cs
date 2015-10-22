using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS
{
    class Text: MapObject
    {
        public string text { get; private set; }
        public Font Font { get; private set; }
        public GeoPoint BeginPoint { get; private set; }
        public SolidBrush sb { get; set; }

        public Text(string text, Font Font, GeoPoint BeginPoint)
        {
            this.text = text;
            this.Font = Font;
            this.BeginPoint = BeginPoint;
            Priority = 1;
        }

        public override bool IsCross(GeoPoint gp, double delta)
        {
            double MaxX = FindMaxCoord().X;
            double MaxY = FindMaxCoord().Y;
            double MinX = BeginPoint.X;
            double MinY = BeginPoint.Y - TextRenderer.MeasureText(text, Font).Height;
            if (gp.X <= MaxX + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                return true;
            else
                return false;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            System.Drawing.Font CurFont = new Font(Font.FontFamily, Font.Size);
            SolidBrush InvertSB = new SolidBrush(Color.FromArgb(sb.Color.A, 0xFF - sb.Color.R, 0xFF - sb.Color.G, 0xFF - sb.Color.B));
            if (Check())
            {
                if (Selected)
                    g.DrawString(text, CurFont, InvertSB, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
                else
                    g.DrawString(text, CurFont, sb, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
            }
            else
                return;
        }

        public override GeoPoint FindMaxCoord()
        {
            SizeF size;
            size = TextRenderer.MeasureText(text, Font);
            size.Height /= (float)CurrentLayer.CurrentMap.MapScale;
            size.Width /= (float)CurrentLayer.CurrentMap.MapScale;
            return new GeoPoint(BeginPoint.X + size.Width, BeginPoint.Y + size.Height);
        }

        public Text(string text, Font Font, double X, double Y)
        {
            this.text = text;
            this.Font = Font;
            this.BeginPoint = new GeoPoint(X, Y);
            Priority = 1;
        }

    }
}
