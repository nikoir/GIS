using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GIS
{
    class Polyline: MapObject
    {
        protected List<GeoPoint> Nodes { get; private set; }
        public Polyline()
        {
            Nodes = new List<GeoPoint>();
        }
        public void AddNode(double X, double Y)
        {
            Nodes.Add(new GeoPoint(X, Y));
        }

        public void AddNode(GeoPoint geoPoint)
        {
            Nodes.Add(geoPoint);
        }

        public void DeleteNode(int index)
        {
            if (index < Nodes.Count)
                Nodes.RemoveAt(index);
            else
                throw new IndexOutOfRangeException();
        }
        public GeoPoint GetNode(int index)
        {
            if (index < Nodes.Count)
                return Nodes[index];
            else
                throw new IndexOutOfRangeException();
        }
        public virtual double Length()
        {
            double length = 0;
            for(int i = 0; i < Nodes.Count - 1; i++)
            {
                length += Math.Sqrt((Nodes[i].X - Nodes[i + 1].X) * (Nodes[i].X - Nodes[i + 1].X) + (Nodes[i].Y - Nodes[i + 1].Y) * (Nodes[i].Y - Nodes[i + 1].Y));
            }
            return length;
        }
        public override void Draw(PaintEventArgs e)
        {
            if (Check())
            {
                Point p1;
                Point p2;
                for (int i = 0; i < Nodes.Count - 1; i++)
                {
                    p1 = CurrentLayer.CurrentMap.MapToScreen(Nodes[i]);
                    p2 = CurrentLayer.CurrentMap.MapToScreen(Nodes[i + 1]);
                    Pen p = new Pen(Color.Black, 5);
                    e.Graphics.DrawLine(p, p1, p2);
                }
            }
            else
                return;
        }
    }
}
