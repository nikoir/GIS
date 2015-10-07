using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GIS
{
    class Line: MapObject
    {
        public GeoPoint GeoPoinBegin { get; private set; }
        public GeoPoint GeoPointEnd { get; private set; }

        public Line(GeoPoint GeoPointBegin, GeoPoint GeoPointEnd)
        {
            this.GeoPoinBegin = GeoPoinBegin;
            this.GeoPointEnd = GeoPointEnd;
        }

        public Line (double XBegin, double YBegin, double XEnd, double YEnd)
        {
            GeoPoinBegin = new GeoPoint(XBegin, YBegin);
            GeoPointEnd = new GeoPoint(XEnd, YEnd);
        }

        public double Length()
        {
            return Math.Sqrt((GeoPoinBegin.X - GeoPointEnd.X) * (GeoPoinBegin.X - GeoPointEnd.X) + (GeoPoinBegin.Y - GeoPointEnd.Y) * (GeoPoinBegin.Y - GeoPointEnd.Y));
        }

        public override void Draw(PaintEventArgs e)
        {
            if (Check())
            {
                Point p1 = CurrentLayer.CurrentMap.MapToScreen(GeoPoinBegin);
                Point p2 = CurrentLayer.CurrentMap.MapToScreen(GeoPointEnd);
                Pen p = new Pen(Color.Black, 5);
                e.Graphics.DrawLine(p, p1, p2);
            }
            else
                return;
        }
    }
}
