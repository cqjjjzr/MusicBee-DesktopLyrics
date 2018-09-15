using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using Unmanaged;

namespace MusicBeePlugin
{
    public partial class FrmLyrics : Form
    {
        private readonly SettingsObj _startupSettings;
        private readonly string _path;
        public FrmLyrics(SettingsObj settings, string path, Form owner)
        {
            _path = path;
            //Owner = owner;
            _startupSettings = settings;
            
            InitializeComponent();
        }
        
        private void FrmLyrics_Load(object sender, EventArgs e)
        {
            var scrBounds = Screen.PrimaryScreen.Bounds;
            Width = scrBounds.Width;
            Height = 150;
            try
            {
                var coord = File.ReadAllText(_path).Split(' ');
                Left = int.Parse(coord[0]);
                Top = int.Parse(coord[1]);
            }
            catch (Exception)
            {
                Top = scrBounds.Height - Height - 150;
                Left = 0;
            }

            TopMost = true;

            UnmanagedHelper.SetTopMost(this);
            UpdateFromSettings(_startupSettings);
        }

        private static readonly Brush ShadowBrush = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
        private const int ShadowOffset = 2;
        public static readonly StringFormat Format = new StringFormat(StringFormatFlags.NoWrap)
        {
            Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near
        };

        
        private Pen _borderPen;
        private Font _mainFont, _subFont;
        private Color _color1, _color2;
        private GredientType _type;
        // Thread unsafe!!!
        public void UpdateFromSettings(SettingsObj settings)
        {
            _mainFont = settings.FontActual;
            _subFont = new Font(_mainFont.FontFamily, _mainFont.Size * 0.8f, _mainFont.Style, _mainFont.Unit, _mainFont.GdiCharSet);
            _borderPen?.Dispose();
            _borderPen = new Pen(settings.BorderColor, 2.5f)
            {
                LineJoin = LineJoin.Round,
                EndCap = LineCap.Round,
                StartCap = LineCap.Round
            };
            _type = (GredientType) settings.GradientType;
            _color1 = settings.Color1;
            _color2 = settings.Color2;

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

        public void DrawLyrics1Line(string lyrics)
        {
            var bitmap = Render1LineLyrics(lyrics);
            if (Width != bitmap.Width) Width = bitmap.Width;
            if (Height != bitmap.Height) Height = bitmap.Height;

            GdiplusHelper.SetBitmap(bitmap, 255, Handle, Left, Top, Width, Height);
            bitmap.Dispose();
        }

        public void DrawLyrics2Line(string line1, string line2)
        {
            var bitmap = Render2LineLyrics(line1, line2);
            if (Width != bitmap.Width) Width = bitmap.Width;
            if (Height != bitmap.Height) Height = bitmap.Height;

            GdiplusHelper.SetBitmap(bitmap, 255, Handle, Left, Top, Width, Height);

            bitmap.Dispose();
        }

        public Bitmap Render1LineLyrics(string line1)
        {
            return RenderLyrics(line1, _mainFont);
        }
        
        public Bitmap Render2LineLyrics(string line1, string line2)
        {
            var line1Bitmap = RenderLyrics(line1, _mainFont);
            var line2Bitmap = RenderLyrics(line2, _subFont);
            var bitmap = new Bitmap(Math.Max(line1Bitmap.Width, line2Bitmap.Width), line1Bitmap.Height + line2Bitmap.Height);
            var g = Graphics.FromImage(bitmap);
            g.DrawImage(line1Bitmap, new PointF(0, 0));
            g.DrawImage(line2Bitmap, new PointF(0, line1Bitmap.Height * 0.8f));
            g.Dispose();
            line1Bitmap.Dispose();
            line2Bitmap.Dispose();
            return bitmap;
        }

        public Bitmap RenderLyrics(string lyric, Font font)
        {
            var fontBounds = TextRenderer.MeasureText(lyric, font);
            var height = fontBounds.Height;
            if (height <= 0) height = 1;
            var width = fontBounds.Width;
            if (width <= 0) width = 1;
            var bitmap = new Bitmap(Width, height);
            var g = Graphics.FromImage(bitmap);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.CompositingQuality = CompositingQuality.HighQuality;
            
            var dstRect = new RectangleF(
                ((float) Width - fontBounds.Width) / 2,
                0,
                width,
                height);
            if (dstRect.X < 0) dstRect.X = 0;
            if (dstRect.Width > Width) dstRect.Width = Width;

            //using (Image shadow = GaussianHelper.CreateShadow())
            //
            var fontEmSize = font.Size;
            var layoutRect = new RectangleF(dstRect.X - ShadowOffset, dstRect.Y - ShadowOffset, dstRect.Width, dstRect.Height);
            var shadowPath = new GraphicsPath(FillMode.Alternate);
            shadowPath.AddString(lyric, font.FontFamily, (int)font.Style, fontEmSize, layoutRect, Format);
            g.FillPath(ShadowBrush, shadowPath);
            shadowPath.Dispose();

            var stringPath = new GraphicsPath(FillMode.Alternate);
            stringPath.AddString(lyric, font.FontFamily, (int)font.Style, fontEmSize, dstRect, Format);
            if (_borderPen != null)
                g.DrawPath(_borderPen, stringPath);
            var stringBrush = CreateGradientBrush(dstRect);
            g.FillPath(stringBrush, stringPath);
            //g.DrawString(lyric, _mainFont, stringBrush, dstRect);
            g.Dispose();
            stringBrush.Dispose();
            stringPath.Dispose();
            return bitmap;
        }

        private void FrmLyrics_MouseDown(object sender, MouseEventArgs e)
        {
            Unmanaged.Unmanaged.ReleaseCapture();
            if (e.Button != MouseButtons.Left) return;
            Unmanaged.Unmanaged.SendMessage(Handle, 0x00A1, new IntPtr(0x0002), null);
        }

        private void FrmLyrics_Move(object sender, EventArgs e)
        {
            File.WriteAllText(_path, $@"{Left} {Top}");
        }

        private void FrmLyrics_MouseUp(object sender, MouseEventArgs e)
        {
            // This event is never fired...?
            //if (e.Button != MouseButtons.Left) return;
            //global::Unmanaged.Unmanaged.SendMessage(Handle, 0x00A2, new IntPtr(0x0002), null);
        }

        private Brush CreateGradientBrush(RectangleF dstRect)
        {
            switch (_type)
            {
                case GredientType.DoubleColor:
                    return new LinearGradientBrush(dstRect.Location, new PointF(dstRect.X, dstRect.Y + dstRect.Height), _color1, _color2)
                    {
                        WrapMode = WrapMode.TileFlipXY
                    };
                case GredientType.TripleColor:
                    return new LinearGradientBrush(dstRect.Location, new PointF(dstRect.X, dstRect.Y + dstRect.Height / 2), _color1, _color2)
                    {
                        WrapMode = WrapMode.TileFlipXY
                    };
                default:
                    return new SolidBrush(_color1);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // This form has to have the WS_EX_LAYERED extended style
                cp.ExStyle |= 0x00000080;
                return cp;
            }
        }
    }
}
