using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GIS
{
    class Polygon: Polyline
    {
        public Brush br { get; set; }
        const double Epsilon = 1E-9;
        public override double Length()
        {
            return base.Length() + Math.Sqrt((Nodes[0].X - Nodes[Nodes.Count - 1].X) * (Nodes[0].X - Nodes[Nodes.Count - 1].X) + (Nodes[0].Y - Nodes[Nodes.Count - 1].Y) * (Nodes[0].Y - Nodes[Nodes.Count - 1].Y));
        }
        public override void Draw(System.Drawing.Graphics g)
        {
            if (Nodes.Count != 0 && Check())
            {
                //SolidBrush InvertSB = new SolidBrush(Color.FromArgb(br.Color.A, 0xFF - br.Color.R, 0xFF - br.Color.G, 0xFF - br.Color.B));
                SolidBrush InvertSB = new SolidBrush(Color.Yellow);
                System.Drawing.Point[] PointArray = new System.Drawing.Point[Nodes.Count];
                for (int i = 0; i < Nodes.Count; i++)
                    PointArray[i] = CurrentLayer.CurrentMap.MapToScreen(Nodes[i]);
                if (Selected)
                    g.FillPolygon(InvertSB, PointArray);
                else
                    g.FillPolygon(br, PointArray);

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
            double Straight1X0 = gp.X;
            double Straight1X1 = CurrentLayer.CurrentMap.FindMaxCoord().X;
            double Straight1Y0 = gp.Y;
            double Straight1Y1 = CurrentLayer.CurrentMap.FindMaxCoord().Y;
            double A1 = Straight1Y1-Straight1Y0;
            double B1 = -(Straight1X1 - Straight1X0);
            double C1 = Straight1Y0*(Straight1X1 - Straight1X0) - Straight1X0*(Straight1Y1 - Straight1Y0);
            //Точка пересечения отрезков
            double IntersectX;
            double IntersectY;
            //Границы для поверки принадлежности прямой отрезку
            double Straight1MinX;
            double Straight1MaxX;
            double Straight1MinY;
            double Straight1MaxY;
            double Straight2MinX;
            double Straight2MaxX;
            double Straight2MinY;
            double Straight2MaxY;

            double Straight2X0;
            double Straight2X1;
            double Straight2Y0;
            double Straight2Y1;
            double A2;
            double B2;
            double C2;

            if (Straight1X0 > Straight1X1)
            {
                Straight1MaxX = Straight1X0;
                Straight1MinX = Straight1X1;
            }
            else
            {
                Straight1MaxX = Straight1X1;
                Straight1MinX = Straight1X0;
            }
            if (Straight1Y0 > Straight1Y1)
            {
                Straight1MaxY = Straight1Y0;
                Straight1MinY = Straight1Y1;
            }
            else
            {
                Straight1MaxY = Straight1Y1;
                Straight1MinY = Straight1Y0;
            }
            //Уравнение второй прямой
            for (int i = 0; i < Nodes.Count; i++)
            {
                Straight2X0 = Nodes[i].X;
                if (i != Nodes.Count - 1)
                    Straight2X1 = Nodes[i + 1].X;
                else
                    Straight2X1 = Nodes[0].X;
                Straight2Y0 = Nodes[i].Y;
                if (i != Nodes.Count - 1)
                    Straight2Y1 = Nodes[i + 1].Y;
                else
                    Straight2Y1 = Nodes[0].Y;

                A2 = Straight2Y1 - Straight2Y0;
                B2 = -(Straight2X1 - Straight2X0);
                C2 = Straight2Y0 * (Straight2X1 - Straight2X0) - Straight2X0 * (Straight2Y1 - Straight2Y0);

                if (Math.Abs(A1 * B2 - A2 * B1) <= Epsilon) //Если прямые параллельны
                    continue;
                else
                {
                    //Находим точку пересечения двух отрезков
                    IntersectX = -(C1 * B2 - C2 * B1) / (A1 * B2 - A2 * B1);
                    IntersectY = -(A1 * C2 - A2 * C1) / (A1 * B2 - A2 * B1);
                    if (Straight2X0 > Straight2X1)
                    {
                        Straight2MaxX = Straight2X0;
                        Straight2MinX = Straight2X1;
                    }
                    else
                    {
                        Straight2MaxX = Straight2X1;
                        Straight2MinX = Straight2X0;
                    }
                    if (Straight2Y0 > Straight2Y1)
                    {
                        Straight2MaxY = Straight2Y0;
                        Straight2MinY = Straight2Y1;
                    }
                    else
                    {
                        Straight2MaxY = Straight2Y1;
                        Straight2MinY = Straight2Y0;
                    }
                    //Сатанинское условие...
                    if (IntersectX >= Straight1MinX && IntersectX >= Straight2MinX && IntersectX <= Straight1MaxX && IntersectX <= Straight2MaxX && IntersectY >= Straight1MinY && IntersectY >= Straight2MinY && IntersectY <= Straight1MaxY && IntersectY <= Straight2MaxY)
                        count++;
                }
            }
            if (count % 2 != 0)
                return true;
            return false;
        }
    }
}
