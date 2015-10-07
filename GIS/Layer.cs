using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS
{
    class Layer
    {
        List<MapObject> MapObjects;
        public string Name { get; set; }
        public bool Visible { get; set; }
        public Map CurrentMap { get; private set; }
        int Order { get; set; } //Порядковый номер слоя
        public Layer()
        {
            MapObjects = new List<MapObject>();
        }
        public void AddMap(Map map)
        {
            this.CurrentMap = map;
        }
    }
}
