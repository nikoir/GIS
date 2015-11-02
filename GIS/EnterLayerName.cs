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
    public partial class EnterLayerName : Form
    {
        public string LayerName { get; set; }
        public EnterLayerName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LayerName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
