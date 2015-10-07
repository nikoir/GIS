using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS
{
    class GeoPoint
    {
        public double X { get; private set; }
        public double Y{ get; private set; }
        public GeoPoint()
        {
            X = 0;
            Y = 0;
        }
        public GeoPoint(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
