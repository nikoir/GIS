using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GIS
{
    public class Polygon: Polyline
    {
        public Brush brush;
        public Brush Brush
        {
            get
            {
                return brush;
            }
            set
            {
                brush = value;
                if (Check())
                    CurrentLayer.CurrentMap.Invalidate();
            }
        }
        const double Epsilon = 1E-9;
        public override double Length()
        {
            return base.Length() + Math.Sqrt((Nodes[0].X - Nodes[Nodes.Count - 1].X) * (Nodes[0].X - Nodes[Nodes.Count - 1].X) + (Nodes[0].Y - Nodes[Nodes.Count - 1].Y) * (Nodes[0].Y - Nodes[Nodes.Count - 1].Y));
        }
        public override void Draw(System.Drawing.Graphics g)
        {
            if (Nodes.Count != 0 && Check())
            {
                System.Drawing.Point[] PointArray = new System.Drawing.Point[Nodes.Count];
                for (int i = 0; i < Nodes.Count; i++)
                    PointArray[i] = CurrentLayer.CurrentMap.MapToScreen(Nodes[i]);
                g.FillPolygon(Brush, PointArray);
                if (Selected)
                {
                    Pen InvertPen = new Pen(Color.FromArgb(Pen.Color.A, 0xFF - Pen.Color.R, 0xFF - Pen.Color.G, 0xFF - Pen.Color.B), Pen.Width);
                    InvertPen.DashStyle = Pen.DashStyle;
                    g.DrawPolygon(InvertPen, PointArray);
                }
                else
                    g.DrawPolygon(Pen, PointArray);

            }
            else
                return;
        }
        public Polygon()
        {
            Priority = 4;
        }
        public override bool IsCross(GeoPoint gp, double delta) //Даже не пытайтесь в этом разобраться...
        {
            int count = 0; //счетчик для подсчета количества пересечений луча
            //Уравнение первой прямой
            double X0_1 = gp.X;
            double X1_1 = CurrentLayer.MaxCoords.X;
            double Y0_1 = gp.Y;
            double Y1_1 = CurrentLayer.MaxCoords.Y;
            double A1 = Y1_1 - Y0_1;
            double B1 = X0_1 - X1_1;
            double C1 = Y0_1*(X1_1 - X0_1) + X0_1*(Y0_1 - Y1_1);
            //Точка пересечения отрезков
            double IntersectX;
            double IntersectY;
            //Границы для поверки принадлежности прямой отрезку

            double MinX_1;
            double MaxX_1;
            double MinY_1;
            double MaxY_1;

            double MinX_2;
            double MaxX_2;
            double MinY_2;
            double MaxY_2;

            double X0_2;
            double X1_2;
            double Y0_2;
            double Y1_2;
            double A2;
            double B2;
            double C2;

            if (X0_1 > X1_1)
            {
                MaxX_1 = X0_1;
                MinX_1 = X1_1;
            }
            else
            {
                MaxX_1 = X1_1;
                MinX_1 = X0_1;
            }
            if (Y0_1 > Y1_1)
            {
                MaxY_1 = Y0_1;
                MinY_1 = Y1_1;
            }
            else
            {
                MaxY_1 = Y1_1;
                MinY_1 = Y0_1;
            }

            //Уравнение второй прямой
            for (int i = 0; i < Nodes.Count; i++)
            {
                X0_2 = Nodes[i].X;
                Y0_2 = Nodes[i].Y;
                if (i != Nodes.Count - 1)
                {
                    X1_2 = Nodes[i + 1].X;
                    Y1_2 = Nodes[i + 1].Y;
                }
                else
                {
                    X1_2 = Nodes[0].X;
                    Y1_2 = Nodes[0].Y;
                }
                A2 = Y1_2 - Y0_2;
                B2 = X0_2 - X1_2;
                C2 = Y0_2 * (X1_2 - X0_2) + X0_2 * (Y0_2 - Y1_2);

                if (Math.Abs(A1 * B2 - A2 * B1) <= Epsilon) //Если прямые параллельны
                    continue;
                else
                {
                    //Находим точку пересечения двух отрезков
                    IntersectX = -(C1 * B2 - C2 * B1) / (A1 * B2 - A2 * B1);
                    IntersectY = -(A1 * C2 - A2 * C1) / (A1 * B2 - A2 * B1);
                    if (X0_2 > X1_2)
                    {
                        MaxX_2 = X0_2;
                        MinX_2 = X1_2;
                    }
                    else
                    {
                        MaxX_2 = X1_2;
                        MinX_2 = X0_2;
                    }
                    if (Y0_2 > Y1_2)
                    {
                        MaxY_2 = Y0_2;
                        MinY_2 = Y1_2;
                    }
                    else
                    {
                        MaxY_2 = Y1_2;
                        MinY_2 = Y0_2;
                    }
                    if ((IntersectX >= MinX_2 - Epsilon && IntersectX <= MaxX_2 + Epsilon && IntersectY >= MinY_2 - Epsilon && IntersectY <= MaxY_2 + Epsilon) 
                        && (IntersectX >= MinX_1 - Epsilon && IntersectX <= MaxX_1 + Epsilon && IntersectY >= MinY_1 - Epsilon && IntersectY <= MaxY_1 + Epsilon))
                        count++;
                }
            }
            if (count % 2 != 0)
                return true;
            return false;
        }
    }
}
