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

        public static Bitmap Render1LineLyrics(string line1)
        {
            return RenderLyrics(line1, _mainFont);
        }

        public static Bitmap Render2LineLyrics(string line1, string line2)
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

        public static Bitmap RenderLyrics(string lyric, Font font)
        {
            var fontBounds = TextRenderer.MeasureText(lyric, font);
            var height = fontBounds.Height;
            if (height <= 0) height = 1;
            var width = fontBounds.Width;
            if (width <= 0) width = 1;
            var bitmap = new Bitmap(_width, height);
            var g = Graphics.FromImage(bitmap);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.CompositingQuality = CompositingQuality.HighQuality;

            var dstRect = new RectangleF(
                ((float)_width - fontBounds.Width) / 2,
                0,
                width,
                height);
            if (dstRect.X < 0) dstRect.X = 0;
            if (dstRect.Width > _width) dstRect.Width = _width;

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

        public static void Main(string[] args)
        {
            UpdateFromSettings(new SettingsObj
            {
                BorderColor = Color.Black,
                Color2 = Color.FromArgb(227, 227, 234),
                Color1 = Color.FromArgb(0xFF, 253, 253, 253),
                FontActual = new Font(new FontFamily("Microsoft Yahei"), 32.0f, FontStyle.Regular, GraphicsUnit.Point),
                GradientType = 2
            }, 720);
            Render2LineLyrics("愛を拾い上げた手の温もりが今もまだ残るのです", "将爱捡起来的手的温度 现在还残留着").Save("ivn.png", ImageFormat.Png);
        }
    }
}
