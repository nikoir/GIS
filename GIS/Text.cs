﻿using System;
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
        public SolidBrush sb { get; set; }

        public Text(string text, Font Font, GeoPoint BeginPoint)
        {
            this.text = text;
            this.Font = Font;
            this.BeginPoint = BeginPoint;
        }

        public override bool IsCross(GeoPoint gp, double delta)
        {
            Graphics context = Graphics.FromHwnd(CurrentLayer.CurrentMap.Handle);
            double MaxX = FindMaxCoord().X;
            double MaxY = FindMaxCoord().Y;
            double MinX = BeginPoint.X;
            double MinY = BeginPoint.Y - context.MeasureString(text, Font).Height;
            if (gp.X <= MaxX + delta && gp.X >= MinX - delta && gp.Y <= MaxY + delta && gp.Y >= MinY - delta)
                return true;
            else
                return false;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            if (Check())
                g.DrawString(text, Font, sb, CurrentLayer.CurrentMap.MapToScreen(BeginPoint));
            else
                return;
        }

        public override void InvertColor()
        {
            sb.Color = Color.FromArgb(sb.Color.A, 0xFF - sb.Color.R, 0xFF - sb.Color.G, 0xFF - sb.Color.B);
        }
        public override GeoPoint FindMaxCoord()
        {
            Graphics context = Graphics.FromHwnd(CurrentLayer.CurrentMap.Handle);
            SizeF size;
            size = context.MeasureString(text, Font);
            return new GeoPoint(BeginPoint.X + size.Width, BeginPoint.Y + size.Height);
        }

        public Text(string text, Font Font, double X, double Y)
        {
            this.text = text;
            this.Font = Font;
            this.BeginPoint = new GeoPoint(X, Y);
        }

    }
}
