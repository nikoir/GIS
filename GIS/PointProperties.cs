using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    public partial class PointProperties : Form
    {
        Point point;
        public Point Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
                label3.Text = point.Font.Size.ToString();
                pictureBox1.BackColor = point.SolidBrush.Color;
            }
        }
        public PointProperties(ref Point Point)
        {
            InitializeComponent();
            this.Point = Point;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point.Font = new Font(Point.Font.FontFamily, Point.Font.Size + 2);
            label3.Text = point.Font.Size.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Point.Font = new Font(Point.Font.FontFamily, Point.Font.Size - 2);
            label3.Text = point.Font.Size.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Point.SolidBrush = new SolidBrush(colorDialog1.Color);
                pictureBox1.BackColor = point.SolidBrush.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
