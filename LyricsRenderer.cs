using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public static class LyricsRenderer
    {
        private static readonly Brush ShadowBrush = new SolidBrush(Color.FromArgb(110, 0, 0, 0));
        private const int ShadowOffset = 2;
        public static readonly StringFormat Format = new StringFormat(StringFormatFlags.NoWrap)
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };

        private static Pen _borderPen;
        private static Font _mainFont, _subFont;
        private static Color _color1, _color2;
        private static GredientType _type;
        private static int _width = 1024;
        private static int _dpi = 72;
        // Thread unsafe!!!
        public static void UpdateFromSettings(SettingsObj settings, int width)
        {
            _mainFont = settings.FontActual;
            _subFont = new Font(_mainFont.FontFamily, _mainFont.Size * 0.8f, _mainFont.Style, _mainFont.Unit, _mainFont.GdiCharSet);
            _borderPen?.Dispose();
            _borderPen = new Pen(settings.BorderColor, 2f)
            {
                LineJoin = LineJoin.Round,
                EndCap = LineCap.Round,
                StartCap = LineCap.Round
            };
            _type = (GredientType)settings.GradientType;
            _color1 = settings.Color1;
            _color2 = settings.Color2;

            _width = width;
        }

        public static void SetDpi(int deviceDpi)
        {
            _dpi = deviceDpi;
        }

        public static Bitmap Render1LineLyrics(string line1, IDeviceContext dc)
        {
            return RenderLyrics(line1, _mainFont, dc);
        }

        public static Bitmap Render2LineLyrics(string line1, string line2, IDeviceContext dc)
        {
            var line1Bitmap = RenderLyrics(line1, _mainFont, dc);
            var line2Bitmap = RenderLyrics(line2, _subFont, dc);
            var bitmap = new Bitmap(Math.Max(line1Bitmap.Width, line2Bitmap.Width), line1Bitmap.Height + line2Bitmap.Height);
            var g = Graphics.FromImage(bitmap);
            g.DrawImage(line1Bitmap, new PointF(0, 0));
            g.DrawImage(line2Bitmap, new PointF(0, line1Bitmap.Height * 0.9f));
            g.Dispose();
            line1Bitmap.Dispose();
            line2Bitmap.Dispose();
            return bitmap;
        }

        public static Bitmap RenderLyrics(string lyric, Font font, IDeviceContext dc)
        {
            // TODO: bounds returned by `TextRenderer.MeasureTex` differs greatly
            //       from bounds created by `GraphicsPath.AddString`, need further
            //       investigation.
            var fontBounds = TextRenderer.MeasureText(dc, lyric, font);
            var height = fontBounds.Height;
            if (height <= 0) height = 1;
            var bitmap = new Bitmap(_width, height);
            bitmap.SetResolution(_dpi, _dpi);
            var g = Graphics.FromImage(bitmap);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.CompositingQuality = CompositingQuality.HighQuality;

            float fontEmSize = font.SizeInPoints * _dpi / 72;

            var initialRect = new RectangleF(0, 0, _width, height);
            var stringPath = new GraphicsPath(FillMode.Alternate);
            stringPath.AddString(lyric, font.FontFamily, (int)font.Style, fontEmSize, initialRect, Format);

            var mat = new Matrix();
            mat.Translate(-ShadowOffset, -ShadowOffset);
            stringPath.Transform(mat);

            g.FillPath(ShadowBrush, stringPath);

            mat.Translate(ShadowOffset * 2, ShadowOffset * 2);
            stringPath.Transform(mat);

            if (_borderPen != null)
                g.DrawPath(_borderPen, stringPath);
            var stringBrush = CreateGradientBrush(initialRect);
            g.FillPath(stringBrush, stringPath);
            //g.DrawString(lyric, _mainFont, stringBrush, dstRect);
            g.Dispose();
            stringBrush.Dispose();
            stringPath.Dispose();
            return bitmap;
        }

        private static Brush CreateGradientBrush(RectangleF dstRect)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_type)
            {
                case GredientType.DoubleColor:
                    return new LinearGradientBrush(dstRect.Location, new PointF(dstRect.X, dstRect.Y + dstRect.Height), _color1, _color2)
                    {
                        WrapMode = WrapMode.TileFlipXY
                    };
                case GredientType.TripleColor:
                    return new LinearGradientBrush(dstRect.Location, new PointF(dstRect.X, dstRect.Y + dstRect.Height / 3 * 2), _color1, _color2)
                    {
                        WrapMode = WrapMode.TileFlipXY
                    };
                default:
                    return new SolidBrush(_color1);
            }
        }
    }
}
