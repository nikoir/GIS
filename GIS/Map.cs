using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    public class Map: UserControl
    {
        public List<Layer> Layers = new List<Layer>();
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
            this.MouseWheel += Map_MouseWheel;
        }

        bool LayerExists(string LayerName)
        {
            foreach (Layer l in Layers)
                if (l.Name == LayerName)
                    return true;
            return false;
        }
        void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            Center = ScreenToMap(new System.Drawing.Point(e.X, e.Y));
            if (e.Delta > 0)
                MapScale *= 2;
            else
                MapScale /= 2;
        }

        public Map()
        {
            MapScale = 1;
            this.MouseClick += Map_MouseClick;
            this.MouseMove += Map_MouseMove;
            this.MouseDown += Map_MouseDown;
            this.MouseWheel += Map_MouseWheel;
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
            MapObject SelectedObject = null;
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
                    if (layer != null)
                        SelectedObject = layer.FindObject(ScreenToMap(p), 2 / MapScale);
                    if (SelectedObject != null)
                        SelectedObject.Selected = !SelectedObject.Selected;
                }
                else
                    return;
            }
        }

        public void EditObjectProperty()
        {
            Form EditProperties;
            System.Drawing.Font OldFont;
            System.Drawing.Color OldColor;
            System.Drawing.Pen OldPen;
            System.Drawing.Brush OldBrush;
            string OldTitle;
            foreach (Layer lr in Layers)
                if (lr.Visible)
                    foreach (MapObject mo in lr.MapObjects)
                        if (mo.Selected)
                        {
                            mo.Selected = false;
                            if (mo is Point)
                            {
                                Point Point = mo as Point;
                                OldFont = Point.Font;
                                OldColor = Point.SolidBrush.Color;
                                EditProperties = new PointProperties(ref Point);
                                if (EditProperties.ShowDialog() != DialogResult.OK)
                                {
                                    Point.Font = OldFont;
                                    Point.SolidBrush.Color = OldColor;
                                }
                            }
                            else
                            {
                                if (mo is Text)
                                {
                                    Text Text = mo as Text;
                                    EditProperties = new TextProperties(ref Text);
                                    OldFont = Text.Font;
                                    OldColor = Text.SolidBrush.Color;
                                    OldTitle = Text.Title;
                                    if (EditProperties.ShowDialog() != DialogResult.OK)
                                    {
                                        Text.Font = OldFont;
                                        Text.SolidBrush = new System.Drawing.SolidBrush(OldColor);
                                        Text.Title = OldTitle;
                                    }
                                }
                                else
                                    if (mo is Polygon)
                                    {
                                        Polygon pg = mo as Polygon;
                                        OldBrush = pg.Brush;
                                        OldPen = pg.Pen;
                                        EditProperties = new PolygonProperties(ref pg);
                                        if (EditProperties.ShowDialog() != DialogResult.OK)
                                        {
                                            pg.Pen = OldPen;
                                            pg.Brush = OldBrush;
                                        }
                                    }
                                    else
                                        if (mo is Polyline)
                                        {
                                            Polyline pl = mo as Polyline;
                                            EditProperties = new LineProperties(ref pl);
                                            OldPen = pl.Pen;
                                            if (EditProperties.ShowDialog() != DialogResult.OK)
                                                pl.Pen = OldPen;
                                        }
                                        else
                                            if (mo is Line)
                                            {
                                                Line pl = mo as Line;
                                                EditProperties = new LineProperties(ref pl);
                                                OldPen = pl.Pen;
                                                if (EditProperties.ShowDialog() != DialogResult.OK)
                                                    pl.Pen = OldPen;
                                            }
                            }
                        }
            Invalidate();
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
            if (Layers.Count != 0)
                foreach (Layer lr in Layers)
                    if (lr.Visible)
                        if (lr.MapObjects.Count != 0)
                            for (int i = lr.MapObjects.Count - 1; i >= 0; i--)
                                if (lr.MapObjects[i].Visibility)
                                    lr.MapObjects[i].Draw(e.Graphics);
        }
        public GeoPoint FindMaxCoord()
        {
            if (Layers.Count != 0)
            {
                GeoPoint MaxCoord = Layers[0].FindMaxCoord();
                GeoPoint gp;
                foreach (Layer lr in Layers)
                {
                    gp = lr.FindMaxCoord();
                    if (gp.X > MaxCoord.X)
                        MaxCoord.X = gp.X;
                    if (gp.Y > MaxCoord.Y)
                        MaxCoord.Y = gp.Y;
                }
                return MaxCoord;
            }
            else
                return null;
        }

        public GeoPoint FindMinCoord()
        {
            if (Layers.Count != 0)
            {
                GeoPoint MinCoord = Layers[0].FindMinCoord();
                GeoPoint gp;
                foreach (Layer lr in Layers)
                {
                    gp = lr.FindMaxCoord();
                    if (gp.X < MinCoord.X)
                        MinCoord.X = gp.X;
                    if (gp.Y < MinCoord.Y)
                        MinCoord.Y = gp.Y;
                }
                return MinCoord;
            }
            else
                return null;
        }
        public void CenterLayers()
        {
            GeoPoint Center = new GeoPoint();
            GeoPoint MaxCoord = FindMaxCoord();
            GeoPoint MinCoord = FindMinCoord();
            double Width = MaxCoord.X - MinCoord.X;
            double Height = MaxCoord.Y - MinCoord.Y;
            Center.X = Width / 2 + MinCoord.X;
            Center.Y = Height / 2 + MinCoord.Y;
            this.Center = Center;
            if (Width > Height)
                MapScale = this.Width / Width;
            else
                MapScale = this.Height / Height;
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
            this.DoubleBuffered = true;

        }
        public void AddLayer(Layer layer)
        {
            if (!LayerExists(layer.Name))
            {
                layer.CurrentMap = this;
                layer.Order = Layers.Count;
                Layers.Add(layer);
                CenterLayers();
            }
            else
                throw new Exception("Layer with this name already exists!");
        }
        public Layer FindLayer (string LayerName)
        {
            foreach (Layer l in Layers)
                if (l.Name == LayerName)
                    return l;
            return null;
        }
        public Layer FindLayer (int index)
        {
            return Layers[index];
        }
        public void DeleteLayer(int index)
        {
            if (Layers != null && Layers.Count != 0)
            {
                Layers.RemoveAt(index);
                Invalidate();
            }
        }
    }
}
