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
        public int Order { get; set; } //Порядковый номер слоя
        public Layer()
        {
            MapObjects = new List<MapObject>();
        }
        public void AddMapObject(MapObject mp)
        {
            mp.CurrentLayer = this;
            MapObjects.Add(mp);
        }
        public GeoPoint FindMaxCoord()
        {
            if (MapObjects.Count != 0)
            {
                double MaxX;
                double MaxY;
                GeoPoint gp = new GeoPoint();
                MaxX = MapObjects[0].FindMaxCoord().X;
                MaxY = MapObjects[0].FindMaxCoord().Y;
                foreach (var MapObject in MapObjects)
                {
                    if (MapObject.FindMaxCoord().X > MaxX)
                        MaxX = MapObject.FindMaxCoord().X;
                    if (MapObject.FindMaxCoord().Y > MaxY)
                        MaxY = MapObject.FindMaxCoord().Y;
                    gp.X = MaxX;
                    gp.Y = MaxY;
                }
                return gp;
            }
            return null;
        }
        public MapObject FindObject (GeoPoint gp, double delta)
        {
            foreach (MapObject m in MapObjects)
                if (m.IsCross(gp, delta))
                    return m;
            return null;
        }
    }
}
