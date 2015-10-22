using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            map1.MapScale *= 1.5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            map1.MapScale /= 1.5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            map1.MapScale = 1;
        }

        private void map1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            map1.EnableSelection = !map1.EnableSelection;
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.Filter = "MIF|*.mif";

            if (OFD.ShowDialog() == DialogResult.OK)
                map1.AddLayer(Parser.Parse(OFD.FileName));
        }
    }
}
