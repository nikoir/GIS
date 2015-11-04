using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    public partial class PolygonProperties : Form
    {
        Polygon editablePolygon;
        
        public Polygon EditablePolygon
        {
            get
            {
                return editablePolygon;
            }
            set
            {
                editablePolygon = value;
                WidthTextBox.Text = editablePolygon.Pen.Width.ToString();
                BorderColorPictureBox.BackColor = editablePolygon.Pen.Color;
                switch (editablePolygon.Pen.DashStyle)
                {
                    case System.Drawing.Drawing2D.DashStyle.Solid:
                        BorderSolidRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.Dot:
                        BorderDotRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.Dash:
                        BorderDashRadioButton.Checked = true;
                        break;
                    case System.Drawing.Drawing2D.DashStyle.DashDot:
                        BorderDashDotRadioButton.Checked = true;
                        break;
                }
                if (editablePolygon.Brush is SolidBrush)
                {
                    ForegroundColorPictureBox.BackColor = (EditablePolygon.Brush as SolidBrush).Color;
                    FillSolidRadioButton.Checked = true;
                }
                else
                    if (editablePolygon.Brush is HatchBrush)
                    {
                        ForegroundColorPictureBox.BackColor = (EditablePolygon.Brush as HatchBrush).ForegroundColor;
                        BackgroundColorPictureBox.BackColor = (EditablePolygon.Brush as HatchBrush).BackgroundColor;
                        switch ((editablePolygon.Brush as HatchBrush).HatchStyle)
                        {
                            case HatchStyle.Horizontal:
                                FillHorizontalRadioButton.Checked = true;
                                break;
                            case HatchStyle.Vertical:
                                FillVerticalRadioButton.Checked = true;
                                break;
                            case HatchStyle.BackwardDiagonal:
                                FillBackwardDiagonalRadioButton.Checked = true;
                                break;
                            case HatchStyle.ForwardDiagonal:
                                FillForwardDiagonalRadioButton.Checked = true;
                                break;
                            case HatchStyle.LargeGrid:
                                FillLargeGridRadioButton.Checked = true;
                                break;
                        }
                    }
            }
        }
        public PolygonProperties(ref Polygon EditablePolygon)
        {
            InitializeComponent();
            this.EditablePolygon = EditablePolygon;
        }

        private void Draw(Graphics g, Brush Brush, int Width, int Height)
        {
            List<System.Drawing.Point> PointList = new List<System.Drawing.Point> { new System.Drawing.Point(0, 0), new System.Drawing.Point(0, Height - 1), new System.Drawing.Point(Width - 1, Height - 1), new System.Drawing.Point(Width - 1, 0) };
            g.FillPolygon(Brush, PointList.ToArray());
        }

        private void Draw(System.Drawing.Drawing2D.DashStyle DashStyle, Graphics g, int Width, int Height)
        {
            Pen p = new Pen(Color.Black, 2);
            p.DashStyle = DashStyle;
            g.DrawLine(p, 0, Height / 2, Width, Height / 2);
        }

        private void ChangeStyle(DashStyle DashStyle)
        {
            Pen Pen = new Pen(EditablePolygon.Pen.Color, EditablePolygon.Pen.Width);
            Pen.DashStyle = DashStyle;
            EditablePolygon.Pen = Pen;
        }
        private void ChangeStyle(HatchStyle HatchStyle)
        {
            Color ForegroundColor = ForegroundColorPictureBox.BackColor;
            Color BackgroundColor = BackgroundColorPictureBox.BackColor;
            switch (HatchStyle)
            {
                case System.Drawing.Drawing2D.HatchStyle.Horizontal:
                    EditablePolygon.Brush = new HatchBrush(System.Drawing.Drawing2D.HatchStyle.Horizontal, ForegroundColor, BackgroundColor);
                    break;
                case System.Drawing.Drawing2D.HatchStyle.Vertical:
                    EditablePolygon.Brush = new HatchBrush(System.Drawing.Drawing2D.HatchStyle.Vertical, ForegroundColor, BackgroundColor);
                    break;
                case System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal:
                    EditablePolygon.Brush = new HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, ForegroundColor, BackgroundColor);
                    break;
                case System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal:
                    EditablePolygon.Brush = new HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, ForegroundColor, BackgroundColor);
                    break;
                case System.Drawing.Drawing2D.HatchStyle.LargeGrid:
                    EditablePolygon.Brush = new HatchBrush(System.Drawing.Drawing2D.HatchStyle.LargeGrid, ForegroundColor, BackgroundColor);
                    break;

            }
        }

        private void FillSolidPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, new SolidBrush(Color.Black), FillSolidPictureBox.Width, FillSolidPictureBox.Height);
        }

        private void FillHorizontalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, new HatchBrush(HatchStyle.Horizontal, Color.Black, Color.White), FillHorizontalPictureBox.Width, FillHorizontalPictureBox.Height);
        }

        private void FillVerticalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, new HatchBrush(HatchStyle.Vertical, Color.Black, Color.White), FillVerticalPictureBox.Width, FillVerticalPictureBox.Height);
        }

        private void FillBackwardDiagonalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.White), FillBackwardDiagonalPictureBox.Width, FillBackwardDiagonalPictureBox.Height);
        }

        private void FillForwardDiagonalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, new HatchBrush(HatchStyle.ForwardDiagonal, Color.Black, Color.White), FillForwardDiagonalPictureBox.Width, FillForwardDiagonalPictureBox.Height);
        }

        private void FillLargeGridPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics, new HatchBrush(HatchStyle.LargeGrid, Color.Black, Color.White), FillLargeGridPictureBox.Width, FillLargeGridPictureBox.Height);
        }

        private void BorderSolidPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(DashStyle.Solid, e.Graphics, BorderSolidPictureBox.Width, BorderSolidPictureBox.Height);
        }

        private void BorderDotPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(DashStyle.Dot, e.Graphics, BorderDotPictureBox.Width, BorderDotPictureBox.Height);
        }

        private void BorderDashPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(DashStyle.Dash, e.Graphics, BorderDashPictureBox.Width, BorderDashPictureBox.Height);
        }

        private void BorderDashDotPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Draw(DashStyle.DashDot, e.Graphics, BorderDashDotPictureBox.Width, BorderDashDotPictureBox.Height);
        }

        private void IncreaseButton_Click(object sender, EventArgs e)
        {
            Pen Pen = new Pen(EditablePolygon.Pen.Color, EditablePolygon.Pen.Width + 2);
            Pen.DashStyle = EditablePolygon.Pen.DashStyle;
            EditablePolygon.Pen = Pen;
            WidthTextBox.Text = EditablePolygon.Pen.Width.ToString();
        }

        private void DecreaseButton_Click(object sender, EventArgs e)
        {
            Pen Pen = new Pen(EditablePolygon.Pen.Color, EditablePolygon.Pen.Width - 2);
            Pen.DashStyle = EditablePolygon.Pen.DashStyle;
            EditablePolygon.Pen = Pen;
            WidthTextBox.Text = EditablePolygon.Pen.Width.ToString();
        }

        private void ChangeBorderColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Pen Pen = new Pen(colorDialog1.Color, EditablePolygon.Pen.Width);
                Pen.DashStyle = EditablePolygon.Pen.DashStyle;
                EditablePolygon.Pen = Pen;
            }
        }

        private void BorderSolidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BorderSolidRadioButton.Checked)
                ChangeStyle(DashStyle.Solid);
        }

        private void BorderDotRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BorderDotRadioButton.Checked)
                ChangeStyle(DashStyle.Dot);
        }

        private void BorderDashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BorderDashRadioButton.Checked)
                ChangeStyle(DashStyle.Dash);
        }

        private void BorderDashDotRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BorderDashDotRadioButton.Checked)
                ChangeStyle(DashStyle.DashDot);
        }

        private void ChangeForegroundColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (EditablePolygon.Brush is SolidBrush)
                    EditablePolygon.Brush = new SolidBrush(colorDialog1.Color);
                else
                    if (EditablePolygon.Brush is HatchBrush)
                    {
                        HatchBrush HatchBrush = EditablePolygon.Brush as HatchBrush;
                        EditablePolygon.Brush = new HatchBrush(HatchBrush.HatchStyle, colorDialog1.Color, HatchBrush.BackgroundColor);
                    }
                ForegroundColorPictureBox.BackColor = colorDialog1.Color;
            }
        }

        private void ChangeBackgroundColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (EditablePolygon.Brush is HatchBrush)
                {
                    HatchBrush HatchBrush = EditablePolygon.Brush as HatchBrush;
                    EditablePolygon.Brush = new HatchBrush(HatchBrush.HatchStyle, HatchBrush.ForegroundColor, colorDialog1.Color);
                }
                BackgroundColorPictureBox.BackColor = colorDialog1.Color;
            }
        }

        private void FillSolidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillSolidRadioButton.Checked)
                EditablePolygon.Brush = new SolidBrush(ForegroundColorPictureBox.BackColor);
        }

        private void FillHorizontalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillHorizontalRadioButton.Checked)
                ChangeStyle(HatchStyle.Horizontal);
        }

        private void FillVerticalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillVerticalRadioButton.Checked)
                ChangeStyle(HatchStyle.Vertical);
        }

        private void FillBackwardDiagonalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillBackwardDiagonalRadioButton.Checked)
                ChangeStyle(HatchStyle.BackwardDiagonal);
        }

        private void FillForwardDiagonalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillForwardDiagonalRadioButton.Checked)
                ChangeStyle(HatchStyle.ForwardDiagonal);
        }

        private void FillLargeGridRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillLargeGridRadioButton.Checked)
                ChangeStyle(HatchStyle.LargeGrid);
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
