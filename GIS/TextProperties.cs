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
    public partial class TextProperties : Form
    {
        Text editableText;
        public Text EditableText
        {
            get
            {
                return editableText;
            }
            set
            {
                editableText = value;
                ColorPictureBox.BackColor = editableText.SolidBrush.Color;
                FontTextBox.Text = editableText.Font.FontFamily.Name;
                SizeTextBox.Text = editableText.Font.Size.ToString();
                StyleTextBox.Text = editableText.Font.Style.ToString();
                TitleTextBox.Text = editableText.Title;


            }
        }
        public TextProperties(ref Text EditableText)
        {
            InitializeComponent();
            this.EditableText = EditableText;
        }

        private void ChangeColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                EditableText.SolidBrush = new SolidBrush(colorDialog1.Color);
                ColorPictureBox.BackColor = colorDialog1.Color;
            }
        }

        private void ChangeFontButton_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EditableText.Font = fontDialog1.Font;
                FontTextBox.Text = fontDialog1.Font.ToString();
                FontTextBox.Text = editableText.Font.FontFamily.Name;
                SizeTextBox.Text = editableText.Font.Size.ToString();
                StyleTextBox.Text = editableText.Font.Style.ToString();
            }
        }

        private void TextTextBox_TextChanged(object sender, EventArgs e)
        {
            EditableText.Title = TitleTextBox.Text;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
