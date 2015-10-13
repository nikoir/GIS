﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GIS
{
    class Point: MapObject
    {
        public Font Font { get; set; }
        public char Symbol { get; set; }
        public GeoPoint BeginPoint { get; private set; }
        public SolidBrush sb { get; set; }
        public Point (double X, double Y, char Symbol)
        {
            BeginPoint = new GeoPoint(X, Y);
            this.Symbol = Symbol;
        }
        public Point(GeoPoint BeginPoint, char Symbol)
        {
            this.BeginPoint = BeginPoint;
            this.Symbol = Symbol;
        }

        public override bool IsCross(GeoPoint gp, double delta)
        {
            Graphics context = Graphics.FromHwnd(CurrentLayer.CurrentMap.Handle);
            double MaxX = FindMaxCoord().X;
            double MaxY = FindMaxCoord().Y;
            double MinX = BeginPoint.X;
            double MinY = BeginPoint.Y - context.MeasureString(new string(Symbol, 1), Font).Height;
            if (gp.X <= MaxX + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                return true;
            else
                return false;
        }

        public override void InvertColor()
        {
            sb.Color = Color.FromArgb(sb.Color.A, 0xFF - sb.Color.R, 0xFF - sb.Color.G, 0xFF - sb.Color.B);
        }

        public override void Draw(Graphics g)
        {
            if (Check())
                g.DrawString(new string(Symbol, 1), Font, sb, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
            else
                return;
        }

        public override GeoPoint FindMaxCoord()
        {
            Graphics context = Graphics.FromHwnd(CurrentLayer.CurrentMap.Handle);
            SizeF size;
            size = context.MeasureString(new string(Symbol, 1), Font);
            return new GeoPoint(BeginPoint.X + size.Width, BeginPoint.Y + size.Height);
        }
    }
}