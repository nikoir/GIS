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
        public override double Length()
        {
            return base.Length() + Math.Sqrt((Nodes[0].X - Nodes[Nodes.Count - 1].X) * (Nodes[0].X - Nodes[Nodes.Count - 1].X) + (Nodes[0].Y - Nodes[Nodes.Count - 1].Y) * (Nodes[0].Y - Nodes[Nodes.Count - 1].Y));
        }
        public override void Draw(PaintEventArgs e)
        {
            if (Nodes.Count != 0 && Check())
            {
                Point[] PointArray = new Point[Nodes.Count];
                for (int i = 0; i < Nodes.Count; i++)
                    PointArray[i] = CurrentLayer.CurrentMap.MapToScreen(Nodes[i]);
                e.Graphics.FillPolygon(new SolidBrush(Color.Red), PointArray);

            }
            else
                return;
        }
    }
}
