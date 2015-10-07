using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS
{
    abstract class MapObject
    {
        public bool Visibility { get; set; }
        public bool Salience { get; set; }
        public Layer CurrentLayer { get; set; }
        //public void AddLayer(Layer layer)
        //{
        //    this.CurrentLayer = layer;
        //}

        protected bool Check()
        {
            if (CurrentLayer == null || CurrentLayer.CurrentMap == null)
                return false;
            return true;
        }
        abstract public void Draw(System.Drawing.Graphics g);
    }
}
