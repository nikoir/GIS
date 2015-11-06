using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GIS
{
    public class Point: MapObject
    {
        Font font;
        SolidBrush solidBrush;
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
        public char Symbol { get; set; }
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
        public Point (double X, double Y, char Symbol)
        {
            BeginPoint = new GeoPoint(X, Y);
            this.Symbol = Symbol;
            Priority = 0;
        }
        public Point(GeoPoint BeginPoint, char Symbol)
        {
            this.BeginPoint = BeginPoint;
            this.Symbol = Symbol;
            Priority = 0;
        }

        public override bool IsCross(GeoPoint gp, double delta)
        {
            if (Check())
            {
                double MaxX = FindMaxCoord().X;
                double MaxY = FindMaxCoord().Y;
                double MinX = BeginPoint.X;
                double MinY = BeginPoint.Y - TextRenderer.MeasureText(new String(Symbol, 1), Font).Height / (float)CurrentLayer.CurrentMap.MapScale;
                if (gp.X <= MaxX + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public override void Draw(Graphics g)
        {
            if (Check())
            {
                if (Selected)
                {
                    SolidBrush InvertSB = new SolidBrush(Color.FromArgb(SolidBrush.Color.A, 0xFF - SolidBrush.Color.R, 0xFF - SolidBrush.Color.G, 0xFF - SolidBrush.Color.B));
                    g.DrawString(new string(Symbol, 1), Font, InvertSB, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
                }
                else
                    g.DrawString(new string(Symbol, 1), Font, SolidBrush, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
            }
            else
                return;
        }

        public override GeoPoint FindMaxCoord()
        {
            SizeF size;
            size = TextRenderer.MeasureText(new string(Symbol, 1), Font);
            size.Height /= (float)CurrentLayer.CurrentMap.MapScale;
            size.Width /= (float)CurrentLayer.CurrentMap.MapScale;
            return new GeoPoint(BeginPoint.X + size.Width, BeginPoint.Y);
        }
        public override GeoPoint FindMinCoord()
        {
            SizeF size;
            size = TextRenderer.MeasureText(new string(Symbol, 1), Font);
            size.Height /= (float)CurrentLayer.CurrentMap.MapScale;
            size.Width /= (float)CurrentLayer.CurrentMap.MapScale;
            return new GeoPoint(BeginPoint.X, BeginPoint.Y - size.Height);
        }
    }
}
