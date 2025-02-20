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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.TableLayoutPanel tableLayout;
            System.Windows.Forms.Label label6;
            this.checkBoxHideWhenUnavailable = new System.Windows.Forms.CheckBox();
            this.comboBoxAlignment = new System.Windows.Forms.ComboBox();
            this.checkBoxNextLineWhenNoTranslation = new System.Windows.Forms.CheckBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.checkBoxAutoHide = new System.Windows.Forms.CheckBox();
            this.checkBoxPreserveSlash = new System.Windows.Forms.CheckBox();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.btnBorderColor = new System.Windows.Forms.Button();
            this.comboBoxGradientType = new System.Windows.Forms.ComboBox();
            this.barBackgroundOpacity = new System.Windows.Forms.TrackBar();
            this.dlgFont = new System.Windows.Forms.FontDialog();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            tableLayout = new System.Windows.Forms.TableLayoutPanel();
            label6 = new System.Windows.Forms.Label();
            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barBackgroundOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(17, 60);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(107, 18);
            label1.TabIndex = 2;
            label1.Text = "Text color:";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(17, 136);
            label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(125, 18);
            label3.TabIndex = 5;
            label3.Text = "Border color:";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 173);
            label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(134, 18);
            label2.TabIndex = 7;
            label2.Text = "Gradient Type:";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(17, 22);
            label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(98, 18);
            label4.TabIndex = 0;
            label4.Text = "Text font:";
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(17, 209);
            label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(98, 18);
            label5.TabIndex = 9;
            label5.Text = "Alignment:";
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayout.Controls.Add(label6, 0, 6);
            tableLayout.Controls.Add(label4, 0, 0);
            tableLayout.Controls.Add(this.checkBoxHideWhenUnavailable, 0, 10);
            tableLayout.Controls.Add(this.comboBoxAlignment, 1, 5);
            tableLayout.Controls.Add(this.checkBoxNextLineWhenNoTranslation, 0, 9);
            tableLayout.Controls.Add(this.btnFont, 1, 0);
            tableLayout.Controls.Add(this.checkBoxAutoHide, 0, 8);
            tableLayout.Controls.Add(label5, 0, 5);
            tableLayout.Controls.Add(this.checkBoxPreserveSlash, 0, 7);
            tableLayout.Controls.Add(label1, 0, 1);
            tableLayout.Controls.Add(this.btnColor1, 1, 1);
            tableLayout.Controls.Add(this.btnColor2, 1, 2);
            tableLayout.Controls.Add(label3, 0, 3);
            tableLayout.Controls.Add(this.btnBorderColor, 1, 3);
            tableLayout.Controls.Add(this.comboBoxGradientType, 1, 4);
            tableLayout.Controls.Add(label2, 0, 4);
            tableLayout.Controls.Add(this.barBackgroundOpacity, 1, 6);
            tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayout.Location = new System.Drawing.Point(0, 0);
            tableLayout.Margin = new System.Windows.Forms.Padding(0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new System.Windows.Forms.Padding(12);
            tableLayout.RowCount = 11;
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayout.Size = new System.Drawing.Size(553, 456);
            tableLayout.TabIndex = 16;
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(17, 245);
            label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(179, 18);
            label6.TabIndex = 15;
            label6.Text = "Background Opacity:";
            // 
            // checkBoxHideWhenUnavailable
            // 
            this.checkBoxHideWhenUnavailable.AutoSize = true;
            tableLayout.SetColumnSpan(this.checkBoxHideWhenUnavailable, 2);
            this.checkBoxHideWhenUnavailable.Location = new System.Drawing.Point(17, 408);
            this.checkBoxHideWhenUnavailable.Margin = new System.Windows.Forms.Padding(5);
            this.checkBoxHideWhenUnavailable.Name = "checkBoxHideWhenUnavailable";
            this.checkBoxHideWhenUnavailable.Size = new System.Drawing.Size(421, 22);
            this.checkBoxHideWhenUnavailable.TabIndex = 11;
            this.checkBoxHideWhenUnavailable.Text = "Hide lyrics window when no lyrics available";
            this.checkBoxHideWhenUnavailable.UseVisualStyleBackColor = true;
            this.checkBoxHideWhenUnavailable.CheckedChanged += new System.EventHandler(this.checkBoxHideWhenUnavailable_CheckedChanged);
            // 
            // comboBoxAlignment
            // 
            this.comboBoxAlignment.AccessibleName = "Alignment";
            this.comboBoxAlignment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlignment.FormattingEnabled = true;
            this.comboBoxAlignment.Items.AddRange(new object[] {
            "Center",
            "Left",
            "Right"});
            this.comboBoxAlignment.Location = new System.Drawing.Point(206, 205);
            this.comboBoxAlignment.Margin = new System.Windows.Forms.Padding(5);
            this.comboBoxAlignment.Name = "comboBoxAlignment";
            this.comboBoxAlignment.Size = new System.Drawing.Size(330, 26);
            this.comboBoxAlignment.TabIndex = 6;
            this.comboBoxAlignment.SelectedIndexChanged += new System.EventHandler(this.comboBoxAlignment_SelectedIndexChanged);
            // 
            // checkBoxNextLineWhenNoTranslation
            // 
            this.checkBoxNextLineWhenNoTranslation.Checked = true;
            this.checkBoxNextLineWhenNoTranslation.CheckState = System.Windows.Forms.CheckState.Checked;
            tableLayout.SetColumnSpan(this.checkBoxNextLineWhenNoTranslation, 2);
            this.checkBoxNextLineWhenNoTranslation.Location = new System.Drawing.Point(17, 342);
            this.checkBoxNextLineWhenNoTranslation.Margin = new System.Windows.Forms.Padding(5);
            this.checkBoxNextLineWhenNoTranslation.Name = "checkBoxNextLineWhenNoTranslation";
            this.checkBoxNextLineWhenNoTranslation.Size = new System.Drawing.Size(430, 56);
            this.checkBoxNextLineWhenNoTranslation.TabIndex = 10;
            this.checkBoxNextLineWhenNoTranslation.Text = "Display the next lyric line on the second line when the lyrics have no translatio" +
    "n\r\n";
            this.checkBoxNextLineWhenNoTranslation.UseVisualStyleBackColor = true;
            this.checkBoxNextLineWhenNoTranslation.CheckedChanged += new System.EventHandler(this.checkBoxNextLineWhenNoTranslation_CheckedChanged);
            // 
            // btnFont
            // 
            this.btnFont.AccessibleName = "Text Font";
            this.btnFont.AutoSize = true;
            this.btnFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFont.Location = new System.Drawing.Point(206, 17);
            this.btnFont.Margin = new System.Windows.Forms.Padding(5);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(330, 28);
            this.btnFont.TabIndex = 1;
            this.btnFont.Text = "Font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // checkBoxAutoHide
            // 
            this.checkBoxAutoHide.AutoSize = true;
            this.checkBoxAutoHide.Checked = true;
            this.checkBoxAutoHide.CheckState = System.Windows.Forms.CheckState.Checked;
            tableLayout.SetColumnSpan(this.checkBoxAutoHide, 2);
            this.checkBoxAutoHide.Location = new System.Drawing.Point(17, 310);
            this.checkBoxAutoHide.Margin = new System.Windows.Forms.Padding(5);
            this.checkBoxAutoHide.Name = "checkBoxAutoHide";
            this.checkBoxAutoHide.Size = new System.Drawing.Size(250, 22);
            this.checkBoxAutoHide.TabIndex = 9;
            this.checkBoxAutoHide.Text = "Hide lyrics when stopped";
            this.checkBoxAutoHide.UseVisualStyleBackColor = true;
            this.checkBoxAutoHide.CheckedChanged += new System.EventHandler(this.checkBoxAutoHide_CheckedChanged);
            // 
            // checkBoxPreserveSlash
            // 
            this.checkBoxPreserveSlash.AutoSize = true;
            tableLayout.SetColumnSpan(this.checkBoxPreserveSlash, 2);
            this.checkBoxPreserveSlash.Location = new System.Drawing.Point(17, 278);
            this.checkBoxPreserveSlash.Margin = new System.Windows.Forms.Padding(5);
            this.checkBoxPreserveSlash.Name = "checkBoxPreserveSlash";
            this.checkBoxPreserveSlash.Size = new System.Drawing.Size(169, 22);
            this.checkBoxPreserveSlash.TabIndex = 8;
            this.checkBoxPreserveSlash.Text = "Leave \'/\' as-is";
            this.checkBoxPreserveSlash.UseVisualStyleBackColor = true;
            this.checkBoxPreserveSlash.CheckedChanged += new System.EventHandler(this.checkBoxPreserveSlash_CheckedChanged);
            // 
            // btnColor1
            // 
            this.btnColor1.AccessibleName = "Text Gridient Color 1";
            this.btnColor1.AutoSize = true;
            this.btnColor1.BackColor = System.Drawing.Color.LightCyan;
            this.btnColor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnColor1.Location = new System.Drawing.Point(206, 55);
            this.btnColor1.Margin = new System.Windows.Forms.Padding(5);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(330, 28);
            this.btnColor1.TabIndex = 2;
            this.btnColor1.UseVisualStyleBackColor = false;
            this.btnColor1.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnColor2
            // 
            this.btnColor2.AccessibleName = "Text Gridient Color 2";
            this.btnColor2.AutoSize = true;
            this.btnColor2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnColor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnColor2.Location = new System.Drawing.Point(206, 93);
            this.btnColor2.Margin = new System.Windows.Forms.Padding(5);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(330, 28);
            this.btnColor2.TabIndex = 3;
            this.btnColor2.UseVisualStyleBackColor = false;
            this.btnColor2.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnBorderColor
            // 
            this.btnBorderColor.AccessibleName = "Text Border Color";
            this.btnBorderColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBorderColor.BackColor = System.Drawing.Color.Gray;
            this.btnBorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBorderColor.Location = new System.Drawing.Point(206, 131);
            this.btnBorderColor.Margin = new System.Windows.Forms.Padding(5);
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.Size = new System.Drawing.Size(330, 28);
            this.btnBorderColor.TabIndex = 4;
            this.btnBorderColor.UseVisualStyleBackColor = false;
            this.btnBorderColor.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // comboBoxGradientType
            // 
            this.comboBoxGradientType.AccessibleName = "Gradient Type";
            this.comboBoxGradientType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGradientType.FormattingEnabled = true;
            this.comboBoxGradientType.Items.AddRange(new object[] {
            "No Gradient",
            "Double Color Gradient",
            "Triple Color Gradient"});
            this.comboBoxGradientType.Location = new System.Drawing.Point(206, 169);
            this.comboBoxGradientType.Margin = new System.Windows.Forms.Padding(5);
            this.comboBoxGradientType.Name = "comboBoxGradientType";
            this.comboBoxGradientType.Size = new System.Drawing.Size(330, 26);
            this.comboBoxGradientType.TabIndex = 5;
            this.comboBoxGradientType.SelectedIndexChanged += new System.EventHandler(this.comboBoxGradientType_SelectedIndexChanged);
            // 
            // barBackgroundOpacity
            // 
            this.barBackgroundOpacity.AccessibleName = "Background Opacity";
            this.barBackgroundOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barBackgroundOpacity.AutoSize = false;
            this.barBackgroundOpacity.Location = new System.Drawing.Point(204, 239);
            this.barBackgroundOpacity.Maximum = 255;
            this.barBackgroundOpacity.Name = "barBackgroundOpacity";
            this.barBackgroundOpacity.Size = new System.Drawing.Size(334, 31);
            this.barBackgroundOpacity.TabIndex = 7;
            this.barBackgroundOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.barBackgroundOpacity.Scroll += new System.EventHandler(this.barBackgroundOpacity_Scroll);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(553, 456);
            this.Controls.Add(tableLayout);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmSettings";
            this.Text = "Desktop Lyrics Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barBackgroundOpacity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FontDialog dlgFont;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Button btnColor1;
        private System.Windows.Forms.Button btnColor2;
        private System.Windows.Forms.Button btnBorderColor;
        private System.Windows.Forms.ComboBox comboBoxGradientType;
        private System.Windows.Forms.CheckBox checkBoxPreserveSlash;
        private System.Windows.Forms.CheckBox checkBoxAutoHide;
        private System.Windows.Forms.CheckBox checkBoxNextLineWhenNoTranslation;
        private System.Windows.Forms.CheckBox checkBoxHideWhenUnavailable;
        private System.Windows.Forms.ComboBox comboBoxAlignment;
        private System.Windows.Forms.TrackBar barBackgroundOpacity;
    }
}