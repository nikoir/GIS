using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS
{
    public class Layer
    {
        public List<MapObject> MapObjects { get; private set; }
        public string Name { get; set; }
        private bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                if (CurrentMap != null)
                    CurrentMap.Invalidate();
            }
        }
        public Map CurrentMap { get; set; }
        public GeoPoint MaxCoords { get; private set; }
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
                GeoPoint MaxCoords = new GeoPoint();
                GeoPoint gp = new GeoPoint();
                MaxX = MapObjects[0].FindMaxCoord().X;
                MaxY = MapObjects[0].FindMaxCoord().Y;
                foreach (var MapObject in MapObjects)
                {
                    MaxCoords = MapObject.FindMaxCoord();
                    if (MaxCoords.X > MaxX)
                        MaxX = MaxCoords.X;
                    if (MaxCoords.Y > MaxY)
                        MaxY = MaxCoords.Y;
                    gp.X = MaxX;
                    gp.Y = MaxY;
                }
                return gp;
            }
            return null;
        }

        public GeoPoint FindMinCoord()
        {
            if (MapObjects.Count != 0)
            {
                double MinX;
                double MinY;
                GeoPoint MinCoords = new GeoPoint();
                GeoPoint gp = new GeoPoint();
                MinX = MapObjects[0].FindMinCoord().X;
                MinY = MapObjects[0].FindMinCoord().Y;
                foreach (var MapObject in MapObjects)
                {
                    MinCoords = MapObject.FindMinCoord();
                    if (MinCoords.X < MinX)
                        MinX = MinCoords.X;
                    if (MinCoords.Y < MinY)
                        MinY = MinCoords.Y;
                    gp.X = MinX;
                    gp.Y = MinY;
                }
                return gp;
            }
            return null;
        }
        public MapObject FindObject (GeoPoint gp, double delta)
        {
            MaxCoords = new GeoPoint(FindMaxCoord().X, FindMaxCoord().Y);
            foreach (MapObject m in MapObjects)
                if (m.IsCross(gp, delta))
                    return m;
            return null;
        }
    }
}
