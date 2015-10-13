using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map1.MapScale = 1;
            map1.Center = new GeoPoint(0, 0);
            Layer lr = new Layer();
            Line ln = new Line(100, -200, 200, 400);
            ln.p = new Pen(Color.Green, 5);
            ln.Visibility = true;
            Polyline pl = new Polyline();
            pl.p = new Pen(Color.Black, 5);
            pl.Visibility = true;
            Polygon pn = new Polygon();
            pn.sb = new SolidBrush(Color.Blue);
            pn.Visibility = true;
            Text txt = new GIS.Text("Томск", new Font("TimesNewRoman", 20), new GeoPoint(300, 300));
            txt.Visibility = true;
            txt.sb = new SolidBrush(Color.Turquoise);
            Point p;
            p = new Point(-200, -200, '*');
            p.sb = new SolidBrush(Color.IndianRed);
            p.Font = new System.Drawing.Font("MapInfo Symbols", 50);
            p.Visibility = true;
            pl.AddNode(-100, 200);
            pl.AddNode(-200, 150);
            pl.AddNode(-300, 200);
            pl.AddNode(-400, 300);
            pn.AddNode(100, 100);
            pn.AddNode(-200, 30);
            pn.AddNode(100, 0);
            pn.AddNode(200, 200);
            pn.AddNode(-300, 50);
            lr.Visible = true;
            lr.AddMapObject(p);
            lr.AddMapObject(txt);
            lr.AddMapObject(ln);
            lr.AddMapObject(pl);
            lr.AddMapObject(pn);
            map1.AddLayer(lr);
            this.Text = "MaxX: " + map1.FindMaxCoord().X.ToString() + "; MaxY: " + map1.FindMaxCoord().Y + ";";
        }
    }
}
