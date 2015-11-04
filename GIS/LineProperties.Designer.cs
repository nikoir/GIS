namespace GIS
{
    partial class LineProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.WidthTextBox = new System.Windows.Forms.TextBox();
            this.IncreaseButton = new System.Windows.Forms.Button();
            this.DecreaseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ColorPictureBox = new System.Windows.Forms.PictureBox();
            this.ChangeColorButton = new System.Windows.Forms.Button();
            this.StylesGroupBox = new System.Windows.Forms.GroupBox();
            this.DashDotPictureBox = new System.Windows.Forms.PictureBox();
            this.DashPictureBox = new System.Windows.Forms.PictureBox();
            this.DotPictureBox = new System.Windows.Forms.PictureBox();
            this.SolidPictureBox = new System.Windows.Forms.PictureBox();
            this.DashDotRadioButton = new System.Windows.Forms.RadioButton();
            this.DashRadioButton = new System.Windows.Forms.RadioButton();
            this.DotRadioButton = new System.Windows.Forms.RadioButton();
            this.SolidRadioButton = new System.Windows.Forms.RadioButton();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPictureBox)).BeginInit();
            this.StylesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DashDotPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DashPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SolidPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width:";
            // 
            // WidthTextBox
            // 
            this.WidthTextBox.Location = new System.Drawing.Point(56, 6);
            this.WidthTextBox.Name = "WidthTextBox";
            this.WidthTextBox.ReadOnly = true;
            this.WidthTextBox.Size = new System.Drawing.Size(33, 20);
            this.WidthTextBox.TabIndex = 4;
            // 
            // IncreaseButton
            // 
            this.IncreaseButton.Location = new System.Drawing.Point(95, 4);
            this.IncreaseButton.Name = "IncreaseButton";
            this.IncreaseButton.Size = new System.Drawing.Size(23, 23);
            this.IncreaseButton.TabIndex = 6;
            this.IncreaseButton.Text = "+";
            this.IncreaseButton.UseVisualStyleBackColor = true;
            this.IncreaseButton.Click += new System.EventHandler(this.IncreaseButton_Click);
            // 
            // DecreaseButton
            // 
            this.DecreaseButton.Location = new System.Drawing.Point(124, 4);
            this.DecreaseButton.Name = "DecreaseButton";
            this.DecreaseButton.Size = new System.Drawing.Size(23, 23);
            this.DecreaseButton.TabIndex = 7;
            this.DecreaseButton.Text = "-";
            this.DecreaseButton.UseVisualStyleBackColor = true;
            this.DecreaseButton.Click += new System.EventHandler(this.DecreaseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Color:";
            // 
            // ColorPictureBox
            // 
            this.ColorPictureBox.Location = new System.Drawing.Point(56, 33);
            this.ColorPictureBox.Name = "ColorPictureBox";
            this.ColorPictureBox.Size = new System.Drawing.Size(33, 20);
            this.ColorPictureBox.TabIndex = 9;
            this.ColorPictureBox.TabStop = false;
            // 
            // ChangeColorButton
            // 
            this.ChangeColorButton.Location = new System.Drawing.Point(95, 33);
            this.ChangeColorButton.Name = "ChangeColorButton";
            this.ChangeColorButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeColorButton.TabIndex = 10;
            this.ChangeColorButton.Text = "Change";
            this.ChangeColorButton.UseVisualStyleBackColor = true;
            this.ChangeColorButton.Click += new System.EventHandler(this.ChangeColorButton_Click);
            // 
            // StylesGroupBox
            // 
            this.StylesGroupBox.Controls.Add(this.DashDotPictureBox);
            this.StylesGroupBox.Controls.Add(this.DashPictureBox);
            this.StylesGroupBox.Controls.Add(this.DotPictureBox);
            this.StylesGroupBox.Controls.Add(this.SolidPictureBox);
            this.StylesGroupBox.Controls.Add(this.DashDotRadioButton);
            this.StylesGroupBox.Controls.Add(this.DashRadioButton);
            this.StylesGroupBox.Controls.Add(this.DotRadioButton);
            this.StylesGroupBox.Controls.Add(this.SolidRadioButton);
            this.StylesGroupBox.Location = new System.Drawing.Point(13, 62);
            this.StylesGroupBox.Name = "StylesGroupBox";
            this.StylesGroupBox.Size = new System.Drawing.Size(165, 117);
            this.StylesGroupBox.TabIndex = 11;
            this.StylesGroupBox.TabStop = false;
            this.StylesGroupBox.Text = "Styles:";
            // 
            // DashDotPictureBox
            // 
            this.DashDotPictureBox.Location = new System.Drawing.Point(76, 92);
            this.DashDotPictureBox.Name = "DashDotPictureBox";
            this.DashDotPictureBox.Size = new System.Drawing.Size(81, 17);
            this.DashDotPictureBox.TabIndex = 12;
            this.DashDotPictureBox.TabStop = false;
            this.DashDotPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DashDotPictureBox_Paint);
            // 
            // DashPictureBox
            // 
            this.DashPictureBox.Location = new System.Drawing.Point(76, 67);
            this.DashPictureBox.Name = "DashPictureBox";
            this.DashPictureBox.Size = new System.Drawing.Size(81, 17);
            this.DashPictureBox.TabIndex = 12;
            this.DashPictureBox.TabStop = false;
            this.DashPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DashPictureBox_Paint);
            // 
            // DotPictureBox
            // 
            this.DotPictureBox.Location = new System.Drawing.Point(76, 44);
            this.DotPictureBox.Name = "DotPictureBox";
            this.DotPictureBox.Size = new System.Drawing.Size(81, 17);
            this.DotPictureBox.TabIndex = 12;
            this.DotPictureBox.TabStop = false;
            this.DotPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DotPictureBox_Paint);
            // 
            // SolidPictureBox
            // 
            this.SolidPictureBox.Location = new System.Drawing.Point(76, 20);
            this.SolidPictureBox.Name = "SolidPictureBox";
            this.SolidPictureBox.Size = new System.Drawing.Size(81, 17);
            this.SolidPictureBox.TabIndex = 4;
            this.SolidPictureBox.TabStop = false;
            this.SolidPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.SolidPictureBox_Paint);
            // 
            // DashDotRadioButton
            // 
            this.DashDotRadioButton.AutoSize = true;
            this.DashDotRadioButton.Location = new System.Drawing.Point(7, 92);
            this.DashDotRadioButton.Name = "DashDotRadioButton";
            this.DashDotRadioButton.Size = new System.Drawing.Size(63, 17);
            this.DashDotRadioButton.TabIndex = 3;
            this.DashDotRadioButton.TabStop = true;
            this.DashDotRadioButton.Text = "dashdot";
            this.DashDotRadioButton.UseVisualStyleBackColor = true;
            this.DashDotRadioButton.CheckedChanged += new System.EventHandler(this.DashDotRadioButton_CheckedChanged);
            // 
            // DashRadioButton
            // 
            this.DashRadioButton.AutoSize = true;
            this.DashRadioButton.Location = new System.Drawing.Point(7, 68);
            this.DashRadioButton.Name = "DashRadioButton";
            this.DashRadioButton.Size = new System.Drawing.Size(48, 17);
            this.DashRadioButton.TabIndex = 2;
            this.DashRadioButton.TabStop = true;
            this.DashRadioButton.Text = "dash";
            this.DashRadioButton.UseVisualStyleBackColor = true;
            this.DashRadioButton.CheckedChanged += new System.EventHandler(this.DashRadioButton_CheckedChanged);
            // 
            // DotRadioButton
            // 
            this.DotRadioButton.AutoSize = true;
            this.DotRadioButton.Location = new System.Drawing.Point(7, 44);
            this.DotRadioButton.Name = "DotRadioButton";
            this.DotRadioButton.Size = new System.Drawing.Size(40, 17);
            this.DotRadioButton.TabIndex = 1;
            this.DotRadioButton.TabStop = true;
            this.DotRadioButton.Text = "dot";
            this.DotRadioButton.UseVisualStyleBackColor = true;
            this.DotRadioButton.CheckedChanged += new System.EventHandler(this.DotRadioButton_CheckedChanged);
            // 
            // SolidRadioButton
            // 
            this.SolidRadioButton.AutoSize = true;
            this.SolidRadioButton.Location = new System.Drawing.Point(7, 20);
            this.SolidRadioButton.Name = "SolidRadioButton";
            this.SolidRadioButton.Size = new System.Drawing.Size(46, 17);
            this.SolidRadioButton.TabIndex = 0;
            this.SolidRadioButton.TabStop = true;
            this.SolidRadioButton.Text = "solid";
            this.SolidRadioButton.UseVisualStyleBackColor = true;
            this.SolidRadioButton.CheckedChanged += new System.EventHandler(this.SolidRadioButton_CheckedChanged);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(13, 186);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 12;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(95, 185);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 13;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // LineProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 213);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.StylesGroupBox);
            this.Controls.Add(this.ChangeColorButton);
            this.Controls.Add(this.ColorPictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecreaseButton);
            this.Controls.Add(this.IncreaseButton);
            this.Controls.Add(this.WidthTextBox);
            this.Controls.Add(this.label2);
            this.Name = "LineProperties";
            this.Text = "Line properties";
            ((System.ComponentModel.ISupportInitialize)(this.ColorPictureBox)).EndInit();
            this.StylesGroupBox.ResumeLayout(false);
            this.StylesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DashDotPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DashPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SolidPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox WidthTextBox;
        private System.Windows.Forms.Button IncreaseButton;
        private System.Windows.Forms.Button DecreaseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ColorPictureBox;
        private System.Windows.Forms.Button ChangeColorButton;
        private System.Windows.Forms.GroupBox StylesGroupBox;
        private System.Windows.Forms.PictureBox DashDotPictureBox;
        private System.Windows.Forms.PictureBox DashPictureBox;
        private System.Windows.Forms.PictureBox DotPictureBox;
        private System.Windows.Forms.PictureBox SolidPictureBox;
        private System.Windows.Forms.RadioButton DashDotRadioButton;
        private System.Windows.Forms.RadioButton DashRadioButton;
        private System.Windows.Forms.RadioButton DotRadioButton;
        private System.Windows.Forms.RadioButton SolidRadioButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ColorDialog colorDialog1;

    }
}