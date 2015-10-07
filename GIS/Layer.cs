using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS
{
    class Layer
    {
        public List<MapObject> MapObjects { get; private set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public Map CurrentMap { get; set; }
        int Order { get; set; } //Порядковый номер слоя
        public Layer()
        {
            MapObjects = new List<MapObject>();
        }
        public void AddMapObject(MapObject mp)
        {
            mp.CurrentLayer = this;
            MapObjects.Add(mp);
        }
        //public void AddMap(Map map)
        //{
        //    this.CurrentMap = map;
        //}
    }
}
