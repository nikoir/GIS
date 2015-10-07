using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    class Map: UserControl
    {
        List<Layer> Layers;
        public GeoPoint Center { get; set; }
        public double Scale { get; set; }
        public Map(GeoPoint Center)
        {
            Layers = new List<Layer>();
            this.Center = Center;
            Scale = 1;
        }
        public Map()
        {
            Scale = 1;
        }
        public System.Drawing.Point MapToScreen(GeoPoint gp)
        {
            System.Drawing.Point point = new System.Drawing.Point();
            point.X = (int)((gp.X - Center.X) * Scale + Width / 2);
            point.Y = (int)(Height / 2 - (gp.Y - Center.Y) * Scale);
            return point;
        }

        //public GeoPoint ScreenToMap(System.Drawing.Point point)
        //{

        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Graphics g = this.CreateGraphics();
            g.Clear(System.Drawing.Color.Gray);
            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, this.Width - 1, this.Height - 1));
            //Отрисовка всех слоев
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
    }
}
