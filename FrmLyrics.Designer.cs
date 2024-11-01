using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Gdiplus;
using Unmanaged;
using Size = System.Drawing.Size;

namespace MusicBeePlugin
{
    partial class FrmLyrics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.SuspendLayout();
            // 
            // FrmLyrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1739, 391);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmLyrics";
            this.ShowInTaskbar = false;
            this.Text = "Desktop Lyrics";
            this.Load += new System.EventHandler(this.FrmLyrics_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLyrics_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}