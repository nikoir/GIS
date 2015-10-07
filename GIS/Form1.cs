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
            map1.Center = new GeoPoint(0, 0);
            Layer lr = new Layer();
            Polyline pl = new Polyline();
            pl.AddNode(2, 2);
            pl.AddNode(-5, -5);
            lr.Visible = true;
            lr.AddMapObject(pl);
            map1.AddLayer(lr);

        }
    }
}
