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
    public partial class Form2 : Form
    {
        public string Name { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Name = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
