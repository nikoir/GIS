using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    public abstract class MapObject
    {
        public bool Visibility { get; set; }
        public bool Salience { get; set; }
        public Layer currentLayer;
        public Layer CurrentLayer
        {
            get
            {
                return currentLayer;
            }
            set
            {
                currentLayer = value;
            }
        }
        public int Priority { get; protected set; }
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
                if (selected == true)
                    foreach (MapObject m in CurrentLayer.MapObjects)
                        if (m != this && m.Selected == true)
                            m.Selected = false;
                CurrentLayer.CurrentMap.Invalidate();
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
    }
}
