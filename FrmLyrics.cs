using System;
using System.Windows.Forms;
using Unmanaged;

namespace MusicBeePlugin
{
    public partial class FrmLyrics : Form
    {
        private SettingsObj _settings;
        public FrmLyrics(SettingsObj settings)
        {
            _settings = settings;
            
            InitializeComponent();
        }
        
        private void FrmLyrics_Load(object sender, EventArgs e)
        {
            // TODO: multi-screen support
            var scrBounds = Screen.PrimaryScreen.Bounds;
            LyricsRenderer.SetDpi(DeviceDpi);
            Width = scrBounds.Width;
            Height = 150;
            if (_settings.PosY < 0)
            {
                Top = scrBounds.Height - Height - 150;
                Left = 0;
            }
            else
            {
                Top = _settings.PosY;
                Left = _settings.PosX;
            }

            TopMost = true;

            UnmanagedHelper.SetTopMost(this);
            UpdateFromSettings(_settings);

            Move += (o, args) =>
            {
                _settings.PosX = Left;
                _settings.PosY = Top;
            };
            FormClosing += (o, args) =>
            {
                if (args.CloseReason == CloseReason.UserClosing)
                    args.Cancel = true;
            };
        }
        
        public void UpdateFromSettings(SettingsObj settings)
        {
            _settings = settings;
            LyricsRenderer.UpdateFromSettings(settings, Width);

            Redraw();
        }

        private void Redraw()
        {
            if (_line1 == null)
            {
                Clear();
                return;
            }

            if (_line2 == null)
            {
                DrawLyrics1Line(_line1);
                return;
            }
            DrawLyrics2Line(_line1, _line2);
        }

        public void Clear()
        {
            if (_line1 != "")
                DrawLyrics1Line("");
            _line1 = "";
        }

        private string _line1, _line2;
        public void UpdateLyrics(string line1, string line2)
        {
            _line1 = line1;
            _line2 = line2;
            Redraw();
        }

        private void DrawLyrics1Line(string lyrics)
        {
            using (var g = CreateGraphics())
            {
                var bitmap = LyricsRenderer.Render1LineLyrics(lyrics, g);

                if (bitmap == null)
                    return;

                using (bitmap)
                {
                    if (Width != bitmap.Width) Width = bitmap.Width;
                    if (Height != bitmap.Height) Height = bitmap.Height;

                    GdiplusHelper.SetBitmap(bitmap, 255, Handle, Left, Top, Width, Height);
                }
            }
        }

        private void DrawLyrics2Line(string line1, string line2)
        {
            using (var g = CreateGraphics())
            {
                var bitmap = LyricsRenderer.Render2LineLyrics(line1, line2, g);

                if (bitmap == null)
                    return;

                using (bitmap)
                {
                    if (Width != bitmap.Width) Width = bitmap.Width;
                    if (Height != bitmap.Height) Height = bitmap.Height;

                    GdiplusHelper.SetBitmap(bitmap, 255, Handle, Left, Top, Width, Height);
                }
            }
        }

        private void FrmLyrics_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Visible) return;
            Unmanaged.Unmanaged.ReleaseCapture();
            if (e.Button != MouseButtons.Left) return;
            Unmanaged.Unmanaged.SendMessage(Handle, 0x00A1, new IntPtr(0x0002), null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // WS_EX_LAYERED
                cp.ExStyle |= 0x00000080; // WS_EX_TOOLWINDOW
                return cp;
            }
        }
    }
}
