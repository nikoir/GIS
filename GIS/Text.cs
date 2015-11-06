using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS
{
    public class Text: MapObject
    {
        string title;
        Font font;
        SolidBrush solidBrush;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                if (Check())
                    CurrentLayer.CurrentMap.Invalidate();
            }
        }
        public Font Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
                if (Check())
                    CurrentLayer.CurrentMap.Invalidate();
            }
        }
        public GeoPoint BeginPoint { get; private set; }
        public SolidBrush SolidBrush
        {
            get
            {
                return solidBrush;
            }
            set 
            {
                solidBrush = value;
                if (Check())
                    CurrentLayer.CurrentMap.Invalidate();
            }
        }

        public Text(string text, Font Font, GeoPoint BeginPoint)
        {
            this.Title = text;
            this.Font = Font;
            this.BeginPoint = BeginPoint;
            Priority = 1;
        }

        public override bool IsCross(GeoPoint gp, double delta)
        {
            if (Check())
            {
                double MaxX = FindMaxCoord().X;
                double MaxY = FindMaxCoord().Y;
                double MinX = BeginPoint.X;
                double MinY = BeginPoint.Y - TextRenderer.MeasureText(Title, Font).Height / (float)CurrentLayer.CurrentMap.MapScale;
                if (gp.X <= MaxX + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            if (Check())
            {
                if (Selected)
                {
                    SolidBrush InvertSB = new SolidBrush(Color.FromArgb(SolidBrush.Color.A, 0xFF - SolidBrush.Color.R, 0xFF - SolidBrush.Color.G, 0xFF - SolidBrush.Color.B));
                    g.DrawString(Title, Font, InvertSB, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
                }
                else
                    g.DrawString(Title, Font, SolidBrush, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
            }
            else
                return;
        }

        public override GeoPoint FindMaxCoord()
        {
            SizeF size;
            size = TextRenderer.MeasureText(Title, Font);
            size.Height /= (float)CurrentLayer.CurrentMap.MapScale;
            size.Width /= (float)CurrentLayer.CurrentMap.MapScale;
            return new GeoPoint(BeginPoint.X + size.Width, BeginPoint.Y);
        }

        public override GeoPoint FindMinCoord()
        {
            SizeF size;
            size = TextRenderer.MeasureText(Title, Font);
            size.Height /= (float)CurrentLayer.CurrentMap.MapScale;
            size.Width /= (float)CurrentLayer.CurrentMap.MapScale;
            return new GeoPoint(BeginPoint.X, BeginPoint.Y - size.Height);
        }

        public Text(string text, Font Font, double X, double Y)
        {
            this.Title = text;
            this.Font = Font;
            this.BeginPoint = new GeoPoint(X, Y);
            Priority = 1;
        }

    }
}
