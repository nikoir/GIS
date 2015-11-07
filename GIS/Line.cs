using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GIS
{
    public class Line: MapObject
    {
        Pen pen;
        public GeoPoint GeoPointBegin { get; private set; }
        public GeoPoint GeoPointEnd { get; private set; }
        public Pen Pen
        {
            get
            {
                return pen;
            }
            set
            {
                pen = value;
                if (Check())
                    CurrentLayer.CurrentMap.Draw();
            }
        }

        public Line(GeoPoint GeoPointBegin, GeoPoint GeoPointEnd)
        {
            this.GeoPointBegin = GeoPointBegin;
            this.GeoPointEnd = GeoPointEnd;
            Priority = 2;
        }

        public Line (double XBegin, double YBegin, double XEnd, double YEnd)
        {
            GeoPointBegin = new GeoPoint(XBegin, YBegin);
            GeoPointEnd = new GeoPoint(XEnd, YEnd);
            Priority = 2;
        }

        public override GeoPoint FindMaxCoord()
        {
            GeoPoint gp = new GeoPoint();
            if (GeoPointBegin.X > GeoPointEnd.X)
                gp.X = GeoPointBegin.X;
            else
                gp.X = GeoPointEnd.X;
            if (GeoPointBegin.Y > GeoPointEnd.Y)
                gp.Y = GeoPointBegin.Y;
            else
                gp.Y = GeoPointEnd.Y;
            return gp;
        }

        public override GeoPoint FindMinCoord()
        {
            GeoPoint gp = new GeoPoint();
            if (GeoPointBegin.X < GeoPointEnd.X)
                gp.X = GeoPointBegin.X;
            else
                gp.X = GeoPointEnd.X;
            if (GeoPointBegin.Y < GeoPointEnd.Y)
                gp.Y = GeoPointBegin.Y;
            else
                gp.Y = GeoPointEnd.Y;
            return gp;
        }

        public override bool IsCross(GeoPoint gp, double delta)
        {
            double result = Math.Abs(((GeoPointBegin.Y - GeoPointEnd.Y) * gp.X + (GeoPointEnd.X - GeoPointBegin.X) * gp.Y + GeoPointBegin.X * GeoPointEnd.Y - GeoPointEnd.X * GeoPointBegin.Y) / Math.Sqrt((GeoPointEnd.X - GeoPointBegin.X) * (GeoPointEnd.X - GeoPointBegin.X) + (GeoPointEnd.Y - GeoPointBegin.Y) * (GeoPointEnd.Y - GeoPointBegin.Y)));
            double MinY;
            double MinX;
            double MaxX = FindMaxCoord().X;
            double MaxY = FindMaxCoord().Y;

            if (GeoPointBegin.X < GeoPointEnd.X)
                MinX = GeoPointBegin.X;
            else
                MinX = GeoPointEnd.X;
            if (GeoPointBegin.Y < GeoPointEnd.Y)
                MinY = GeoPointBegin.Y;
            else
                MinY = GeoPointEnd.Y;
            if (result <= (Pen.Width / (2 * this.CurrentLayer.CurrentMap.MapScale)) + delta && gp.X <= MaxX + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                return true;
            else
                return false;
        }

        public double Length()
        {
            return Math.Sqrt((GeoPointBegin.X - GeoPointEnd.X) * (GeoPointBegin.X - GeoPointEnd.X) + (GeoPointBegin.Y - GeoPointEnd.Y) * (GeoPointBegin.Y - GeoPointEnd.Y));
        }

        public override void Draw(ref System.Drawing.Graphics g)
        {
            if (Check())
            {
                System.Drawing.Point p1 = CurrentLayer.CurrentMap.MapToScreen(GeoPointBegin);
                System.Drawing.Point p2 = CurrentLayer.CurrentMap.MapToScreen(GeoPointEnd);
                if (Selected)
                {
                     Pen InvertPen = new Pen(Color.FromArgb(Pen.Color.A, 0xFF - Pen.Color.R, 0xFF - Pen.Color.G, 0xFF - Pen.Color.B), Pen.Width);
                     g.DrawLine(InvertPen, p1, p2);
                }
                else
                    g.DrawLine(Pen, p1, p2);
            }
            else
                return;
        }
    }
}
