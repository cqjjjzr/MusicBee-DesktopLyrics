namespace MusicBeePlugin
{
    partial class FrmSettings
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
            this.dlgFont = new System.Windows.Forms.FontDialog();
            this.btnFont = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.btnBorderColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxGradientType = new System.Windows.Forms.ComboBox();
            this.checkBoxPreserveSlash = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoHide = new System.Windows.Forms.CheckBox();
            this.checkBoxNextLineWhenNoTranslation = new System.Windows.Forms.CheckBox();
            this.checkBoxHideWhenUnavailable = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(132, 15);
            this.btnFont.Margin = new System.Windows.Forms.Padding(4);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(242, 29);
            this.btnFont.TabIndex = 0;
            this.btnFont.Text = "Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text color:";
            // 
            // btnColor1
            // 
            this.btnColor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColor1.BackColor = System.Drawing.Color.LightCyan;
            this.btnColor1.Location = new System.Drawing.Point(132, 53);
            this.btnColor1.Margin = new System.Windows.Forms.Padding(4);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(242, 29);
            this.btnColor1.TabIndex = 2;
            this.btnColor1.UseVisualStyleBackColor = false;
            this.btnColor1.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnColor2
            // 
            this.btnColor2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColor2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnColor2.Location = new System.Drawing.Point(132, 91);
            this.btnColor2.Margin = new System.Windows.Forms.Padding(4);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(242, 29);
            this.btnColor2.TabIndex = 4;
            this.btnColor2.UseVisualStyleBackColor = false;
            this.btnColor2.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnBorderColor
            // 
            this.btnBorderColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorderColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBorderColor.BackColor = System.Drawing.Color.Gray;
            this.btnBorderColor.Location = new System.Drawing.Point(132, 129);
            this.btnBorderColor.Margin = new System.Windows.Forms.Padding(4);
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.Size = new System.Drawing.Size(242, 29);
            this.btnBorderColor.TabIndex = 6;
            this.btnBorderColor.UseVisualStyleBackColor = false;
            this.btnBorderColor.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 136);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Border color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 170);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Gradient Type:";
            // 
            // cbxGradientType
            // 
            this.cbxGradientType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGradientType.FormattingEnabled = true;
            this.cbxGradientType.Items.AddRange(new object[] {
            "No Gradient",
            "Double Color Gradient",
            "Triple Color Gradient"});
            this.cbxGradientType.Location = new System.Drawing.Point(132, 167);
            this.cbxGradientType.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGradientType.Name = "cbxGradientType";
            this.cbxGradientType.Size = new System.Drawing.Size(242, 23);
            this.cbxGradientType.TabIndex = 8;
            // 
            // checkBoxPreserveSlash
            // 
            this.checkBoxPreserveSlash.AutoSize = true;
            this.checkBoxPreserveSlash.Location = new System.Drawing.Point(16, 204);
            this.checkBoxPreserveSlash.Name = "checkBoxPreserveSlash";
            this.checkBoxPreserveSlash.Size = new System.Drawing.Size(149, 19);
            this.checkBoxPreserveSlash.TabIndex = 9;
            this.checkBoxPreserveSlash.Text = "Leave \'/\' as-is";
            this.checkBoxPreserveSlash.UseVisualStyleBackColor = true;
            this.checkBoxPreserveSlash.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxAutoHide
            // 
            this.checkBoxAutoHide.AutoSize = true;
            this.checkBoxAutoHide.Checked = true;
            this.checkBoxAutoHide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoHide.Location = new System.Drawing.Point(16, 231);
            this.checkBoxAutoHide.Name = "checkBoxAutoHide";
            this.checkBoxAutoHide.Size = new System.Drawing.Size(221, 19);
            this.checkBoxAutoHide.TabIndex = 10;
            this.checkBoxAutoHide.Text = "Hide lyrics when stopped";
            this.checkBoxAutoHide.UseVisualStyleBackColor = true;
            this.checkBoxAutoHide.CheckedChanged += new System.EventHandler(this.checkBoxAutoHide_CheckedChanged);
            // 
            // checkBoxNextLineWhenNoTranslation
            // 
            this.checkBoxNextLineWhenNoTranslation.Checked = true;
            this.checkBoxNextLineWhenNoTranslation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNextLineWhenNoTranslation.Location = new System.Drawing.Point(16, 256);
            this.checkBoxNextLineWhenNoTranslation.Name = "checkBoxNextLineWhenNoTranslation";
            this.checkBoxNextLineWhenNoTranslation.Size = new System.Drawing.Size(358, 47);
            this.checkBoxNextLineWhenNoTranslation.TabIndex = 11;
            this.checkBoxNextLineWhenNoTranslation.Text = "Display the next lyric line on the second line when the lyrics have no translatio" +
    "n\r\n";
            this.checkBoxNextLineWhenNoTranslation.UseVisualStyleBackColor = true;
            this.checkBoxNextLineWhenNoTranslation.CheckedChanged += new System.EventHandler(this.checkBoxNextLineWhenNoTranslation_CheckedChanged);
            // 
            // checkBoxHideWhenUnavailable
            // 
            this.checkBoxHideWhenUnavailable.AutoSize = true;
            this.checkBoxHideWhenUnavailable.Location = new System.Drawing.Point(16, 309);
            this.checkBoxHideWhenUnavailable.Name = "checkBoxHideWhenUnavailable";
            this.checkBoxHideWhenUnavailable.Size = new System.Drawing.Size(373, 19);
            this.checkBoxHideWhenUnavailable.TabIndex = 12;
            this.checkBoxHideWhenUnavailable.Text = "Hide lyrics window when no lyrics available";
            this.checkBoxHideWhenUnavailable.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Text font:";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(392, 363);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxHideWhenUnavailable);
            this.Controls.Add(this.checkBoxNextLineWhenNoTranslation);
            this.Controls.Add(this.checkBoxAutoHide);
            this.Controls.Add(this.checkBoxPreserveSlash);
            this.Controls.Add(this.cbxGradientType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBorderColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnColor2);
            this.Controls.Add(this.btnColor1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFont);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSettings";
            this.Text = "Desktop Lyrics Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FontDialog dlgFont;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnColor1;
        private System.Windows.Forms.Button btnColor2;
        private System.Windows.Forms.Button btnBorderColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxGradientType;
        private System.Windows.Forms.CheckBox checkBoxPreserveSlash;
        private System.Windows.Forms.CheckBox checkBoxAutoHide;
        private System.Windows.Forms.CheckBox checkBoxNextLineWhenNoTranslation;
        private System.Windows.Forms.CheckBox checkBoxHideWhenUnavailable;
        private System.Windows.Forms.Label label4;
    }
}