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
    public partial class MainForm : Form
    {
        public MainForm()
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
            EnterLayerName form;
            Layer lr;
            OFD.Filter = "MIF|*.mif";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                form = new EnterLayerName();
                form.ShowDialog();
                if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    lr = Parser.Parse(OFD.FileName);
                    lr.Name = form.LayerName;
                    try
                    {
                        map1.AddLayer(lr);
                        checkedListBox1.Items.Add(lr.Name);
                        checkedListBox1.SetItemChecked(lr.Order, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            switch (e.NewValue)
            {
                case CheckState.Checked:
                    map1.FindLayer(e.Index).Visible = true;
                    break;
                case CheckState.Unchecked:
                    map1.FindLayer(e.Index).Visible = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            map1.EditObjectProperty();
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                map1.DeleteLayer(checkedListBox1.SelectedIndex);
                checkedListBox1.Items.RemoveAt(checkedListBox1.SelectedIndex);
            }
        }
    }
}
