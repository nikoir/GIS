using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    abstract class MapObject
    {
        public bool Visibility { get; set; }
        public bool Salience { get; set; }
        public Layer CurrentLayer { get; set; }
        bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                CurrentLayer.CurrentMap.Invalidate();
                InvertColor();
            }
        }

        protected bool Check()
        {
            if (CurrentLayer == null || CurrentLayer.CurrentMap == null)
                return false;
            return true;
        }
        abstract public void Draw(System.Drawing.Graphics g);
        abstract public GeoPoint FindMaxCoord();
        abstract public bool IsCross(GeoPoint gp, double delta);
        abstract public void InvertColor();
    }
}
