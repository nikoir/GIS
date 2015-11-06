using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GIS
{
    public class Polyline: MapObject
    {
        Pen pen;
        protected List<GeoPoint> Nodes { get; private set; }
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
                    CurrentLayer.CurrentMap.Invalidate();
            }
        }
        public Polyline()
        {
            Nodes = new List<GeoPoint>();
            Priority = 3;
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

        public override bool IsCross(GeoPoint gp, double delta)
        {
            double result;
            double MinY;
            double MinX;
            double MaxX;
            double MaxY;
            for (int i = 0; i < Nodes.Count - 1; i++)
            {
                result = Math.Abs(((Nodes[i].Y - Nodes[i + 1].Y) * gp.X + (Nodes[i + 1].X - Nodes[i].X) * gp.Y + Nodes[i].X * Nodes[i + 1].Y - Nodes[i + 1].X * Nodes[i].Y) / Math.Sqrt((Nodes[i + 1].X - Nodes[i].X) * (Nodes[i + 1].X - Nodes[i].X) + (Nodes[i + 1].Y - Nodes[i].Y) * (Nodes[i + 1].Y - Nodes[i].Y)));
                if (Nodes[i].X > Nodes[i + 1].X)
                {
                    MaxX = Nodes[i].X;
                    MinX = Nodes[i + 1].X;
                }
                else
                {
                    MaxX = Nodes[i + 1].X;
                    MinX = Nodes[i].X;
                }
                if (Nodes[i].Y > Nodes[i + 1].Y)
                {
                    MaxY = Nodes[i].Y;
                    MinY = Nodes[i + 1].Y;
                }
                else
                {
                    MaxY = Nodes[i + 1].Y;
                    MinY = Nodes[i].Y;
                }
                if (result <= (Pen.Width / (2 * this.CurrentLayer.CurrentMap.MapScale)) + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                    return true;
            }
            return false;
        }

        public override GeoPoint FindMaxCoord()
        {
            if (Nodes.Count != 0)
            {
                GeoPoint gp = new GeoPoint();
                double MaxX = Nodes[0].X;
                double MaxY = Nodes[0].Y;
                foreach (var Node in Nodes)
                {
                    if (Node.X > MaxX)
                        MaxX = Node.X;
                    if (Node.Y > MaxY)
                        MaxY = Node.Y;
                }
                gp.X = MaxX;
                gp.Y = MaxY;
                return gp;
            }
            else
                return null;
        }

        public override GeoPoint FindMinCoord()
        {
            if (Nodes.Count != 0)
            {
                GeoPoint gp = new GeoPoint();
                double MinX = Nodes[0].X;
                double MinY = Nodes[0].Y;
                foreach (var Node in Nodes)
                {
                    if (Node.X < MinX)
                        MinX = Node.X;
                    if (Node.Y < MinY)
                        MinY = Node.Y;
                }
                gp.X = MinX;
                gp.Y = MinY;
                return gp;
            }
            else
                return null;
        }

        public virtual double Length()
        {
            double length = 0;
            for(int i = 0; i < Nodes.Count - 1; i++)
                length += Math.Sqrt((Nodes[i].X - Nodes[i + 1].X) * (Nodes[i].X - Nodes[i + 1].X) + (Nodes[i].Y - Nodes[i + 1].Y) * (Nodes[i].Y - Nodes[i + 1].Y));
            return length;
        }
        public override void Draw(System.Drawing.Graphics g)
        {
            if (Check())
            {
                System.Drawing.Point p1;
                System.Drawing.Point p2;
                for (int i = 0; i < Nodes.Count - 1; i++)
                {
                    p1 = CurrentLayer.CurrentMap.MapToScreen(Nodes[i]);
                    p2 = CurrentLayer.CurrentMap.MapToScreen(Nodes[i + 1]);
                    if (Selected)
                    {
                        Pen InvertPen = new Pen(Color.FromArgb(Pen.Color.A, 0xFF - Pen.Color.R, 0xFF - Pen.Color.G, 0xFF - Pen.Color.B), Pen.Width);
                        InvertPen.DashStyle = Pen.DashStyle;
                        g.DrawLine(InvertPen, p1, p2);
                    }
                    else
                        g.DrawLine(Pen, p1, p2);
                }
            }
            else
                return;
        }
    }
}
