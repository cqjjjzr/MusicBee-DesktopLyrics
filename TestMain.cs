using System;
using System.Drawing;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public class TestMain
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new FrmLyrics(new SettingsObj
            {
                BorderColor = Color.Black,
                Color1 = Color.GhostWhite,
                Color2 = Color.LightGray,
                FontActual = new Font(new FontFamily("Microsoft Yahei"), 34.0f, FontStyle.Regular, GraphicsUnit.Point),
                GradientType = 1
            }, "x.set"));
        }
    }
}
