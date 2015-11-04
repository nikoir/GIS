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
    public partial class LineProperties : Form
    {
        Line editableLine;
        Polyline editablePolyline;

        public Line EditableLine
        {
            get
            {
                return editableLine;
            }
            set
            {
                editableLine = value;
                WidthTextBox.Text = editableLine.Pen.Width.ToString();
                ColorPictureBox.BackColor = editableLine.Pen.Color;
                switch (editableLine.Pen.DashStyle)
                {
                    case System.Drawing.Drawing2D.DashStyle.Solid:
                        SolidRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.Dot:
                        DotRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.Dash:
                        DashRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.DashDot:
                        DashDotRadioButton.Checked = true;
                        break;
                }
            }
        }
        public Polyline EditablePolyline
        {
            get
            {
                return editablePolyline;
            }
            set
            {
                editablePolyline = value;
                WidthTextBox.Text = editablePolyline.Pen.Width.ToString();
                ColorPictureBox.BackColor = editablePolyline.Pen.Color;
                switch (editablePolyline.Pen.DashStyle)
                {
                    case System.Drawing.Drawing2D.DashStyle.Solid:
                        SolidRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.Dot:
                        DotRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.Dash:
                        DashRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.DashDot:
                        DashDotRadioButton.Checked = true;
                        break;
                }
            }
        }
        public LineProperties(ref Line EditableLine)
        {
            InitializeComponent();
            this.EditableLine = EditableLine;
        }
        public LineProperties(ref Polyline EditablePolyline)
        {
            InitializeComponent();
            this.EditablePolyline = EditablePolyline;
        }

        private void Draw(System.Drawing.Drawing2D.DashStyle DashStyle, Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            p.DashStyle = DashStyle;
            g.DrawLine(p, 0, DotPictureBox.Height / 2, DotPictureBox.Width, DotPictureBox.Height / 2);
        }

        private void ChangeStyle(System.Drawing.Drawing2D.DashStyle DashStyle)
        {
            Pen Pen;
            if (EditableLine != null)
            {
                Pen = new Pen(EditableLine.Pen.Color, EditableLine.Pen.Width);
                Pen.DashStyle = DashStyle;
                EditableLine.Pen = Pen;
            }
            else
                if (EditablePolyline != null)
                {
                    Pen = new Pen(EditablePolyline.Pen.Color, EditablePolyline.Pen.Width);
                    Pen.DashStyle = DashStyle;
                    EditablePolyline.Pen = Pen;
                }
        }
        private void SolidPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw(System.Drawing.Drawing2D.DashStyle.Solid, g);
        }

        private void DotPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw(System.Drawing.Drawing2D.DashStyle.Dot, g);
        }

        private void DashPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw(System.Drawing.Drawing2D.DashStyle.Dash, g);
        }

        private void DashDotPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw(System.Drawing.Drawing2D.DashStyle.DashDot, g);
        }

        private void IncreaseButton_Click(object sender, EventArgs e)
        {
            Pen Pen;
            if (EditableLine != null)
            {
                Pen = new Pen(EditableLine.Pen.Color, EditableLine.Pen.Width + 2);
                Pen.DashStyle = EditableLine.Pen.DashStyle;
                EditableLine.Pen = Pen;
                WidthTextBox.Text = EditableLine.Pen.Width.ToString();
            }
            else
                if (EditablePolyline != null)
                {
                    Pen = new Pen(EditablePolyline.Pen.Color, EditablePolyline.Pen.Width + 2);
                    Pen.DashStyle = EditablePolyline.Pen.DashStyle;
                    EditablePolyline.Pen = Pen;
                    WidthTextBox.Text = EditablePolyline.Pen.Width.ToString();
                }
        }

        private void ChangeColorButton_Click(object sender, EventArgs e)
        {
            Pen Pen;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (EditableLine != null)
                {
                    Pen = new Pen(colorDialog1.Color, EditableLine.Pen.Width);
                    Pen.DashStyle = EditableLine.Pen.DashStyle;
                    EditableLine.Pen = Pen;
                    ColorPictureBox.BackColor = colorDialog1.Color;
                }
                else
                    if (EditablePolyline != null)
                    {
                        Pen = new Pen(colorDialog1.Color, EditablePolyline.Pen.Width);
                        Pen.DashStyle = EditablePolyline.Pen.DashStyle;
                        EditablePolyline.Pen = Pen;
                        ColorPictureBox.BackColor = colorDialog1.Color;
                    }
            }
        }

        private void SolidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SolidRadioButton.Checked)
                ChangeStyle(System.Drawing.Drawing2D.DashStyle.Solid);
        }

        private void DotRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DotRadioButton.Checked)
                ChangeStyle(System.Drawing.Drawing2D.DashStyle.Dot);
        }

        private void DashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashRadioButton.Checked)
                ChangeStyle(System.Drawing.Drawing2D.DashStyle.Dash);
        }

        private void DashDotRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashDotRadioButton.Checked)
                ChangeStyle(System.Drawing.Drawing2D.DashStyle.DashDot);
        }

        private void DecreaseButton_Click(object sender, EventArgs e)
        {
            Pen Pen;
            if (EditableLine != null)
            {
                Pen = new Pen(EditableLine.Pen.Color, EditableLine.Pen.Width - 2);
                Pen.DashStyle = EditableLine.Pen.DashStyle;
                EditableLine.Pen = Pen;
                WidthTextBox.Text = EditableLine.Pen.Width.ToString();
            }
            else
                if (EditablePolyline != null)
                {
                    Pen = new Pen(EditablePolyline.Pen.Color, EditablePolyline.Pen.Width - 2);
                    Pen.DashStyle = EditablePolyline.Pen.DashStyle;
                    EditablePolyline.Pen = Pen;
                    WidthTextBox.Text = EditablePolyline.Pen.Width.ToString();
                }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
