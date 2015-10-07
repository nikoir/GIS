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

        public Text(string text, Font Font, GeoPoint BeginPoint)
        {
            this.text = text;
            this.Font = Font;
            this.BeginPoint = BeginPoint;
        }

        public override void Draw(PaintEventArgs e)
        {
            if (Check())
                e.Graphics.DrawString(text, new Font("Arial", 14), new SolidBrush(Color.Black), CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
            else
                return;
        }

        public Text(string text, Font Font, double X, double Y)
        {
            this.text = text;
            this.Font = Font;
            this.BeginPoint = new GeoPoint(X, Y);
        }

    }
}
