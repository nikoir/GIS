﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    class Map: UserControl
    {
        List<Layer> Layers = new List<Layer>();
        GeoPoint center;
        public bool EnableSelection { get; set; }

        public GeoPoint Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
                Invalidate();
            }
        }
        double mapScale;
        int CurX;
        int CurY;
        public double MapScale
        {
            get
            {
                return mapScale;
            }
            set
            {
                mapScale = value;
                Invalidate();
            }
        }
        public Map(GeoPoint Center)
        {
            Layers = new List<Layer>();
            this.Center = Center;
            MapScale = 1;
            this.MouseClick += Map_MouseClick;
            this.MouseMove += Map_MouseMove;
            this.MouseDown += Map_MouseDown;
        }

        public Map()
        {
            MapScale = 1;
            this.MouseClick += Map_MouseClick;
            this.MouseMove += Map_MouseMove;
            this.MouseDown += Map_MouseDown;
        }

        void Map_MouseDown(object sender, MouseEventArgs e)
        {
            CurX = e.X;
            CurY = e.Y;
        }
        void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Center.X += (CurX - e.X)/MapScale;
                Center.Y -= (CurY - e.Y)/MapScale;
                CurX = e.X;
                CurY = e.Y;
                this.Invalidate();
            }
        }
        void Map_MouseClick(object sender, MouseEventArgs e)
        {
            System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
            MapObject SelectedObject;
            if (EnableSelection)
            {
                if (Layers.Count != 0)
                {
                    Layer layer = null;
                    foreach (Layer l in Layers)
                        if (l.Visible)
                        {
                            layer = l;
                            break;
                        }
                    SelectedObject = layer.FindObject(ScreenToMap(p), 2 / MapScale);
                    if (SelectedObject != null)
                        SelectedObject.Selected = !SelectedObject.Selected;
                }
                else
                    return;
            }
        }

        public System.Drawing.Point MapToScreen(GeoPoint gp)
        {
            System.Drawing.Point point = new System.Drawing.Point();
            point.X = (int)((gp.X - Center.X) * MapScale + Width / 2);
            point.Y = (int)(Height / 2 - (gp.Y - Center.Y) * MapScale);
            return point;
        }

        public GeoPoint ScreenToMap(System.Drawing.Point point)
        {
            GeoPoint gp = new GeoPoint();
            gp.X = (point.X - Width / 2) / MapScale + Center.X;
            gp.Y = (Height / 2 - point.Y) / MapScale + Center.Y;
            return gp;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            if (Layers.Count != 0)
                foreach (Layer lr in Layers)
                    if (lr.Visible)
                        if (lr.MapObjects.Count != 0)
                            for (int i = lr.MapObjects.Count - 1; i >= 0; i--)
                                if (lr.MapObjects[i].Visibility)
                                    lr.MapObjects[i].Draw(g);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Name = "Map";
            this.Size = new System.Drawing.Size(153, 151);
            this.ResumeLayout(false);

        }
        public void AddLayer(Layer layer)
        {
            layer.CurrentMap = this;
            layer.Order = Layers.Count;
            Layers.Add(layer);
        }
    }
}
