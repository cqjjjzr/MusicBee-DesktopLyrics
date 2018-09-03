using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using Gdiplus;
using Point = Gdiplus.Point;
using Size = Gdiplus.Size;

namespace MusicBeePlugin
{
    public static class GdiplusHelper
    {
        public static IntPtr GetHBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
            return bitmap.GetHbitmap(Color.FromArgb(0));
        }

        public static void ReleaseHBitmap(IntPtr hBitmap)
        {
            if (hBitmap != IntPtr.Zero) GdiplusPInvoke.DeleteObject(hBitmap);
        }

        public static void SetBitmap(Bitmap bitmap, byte opacity, IntPtr handle, int left, int top, int width, int height)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            var screenDc = GdiplusPInvoke.GetDC(IntPtr.Zero);
            var memDc = GdiplusPInvoke.CreateCompatibleDC(screenDc);
            var oldBitmap = IntPtr.Zero;
            var hBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = GdiplusPInvoke.SelectObject(memDc, hBitmap);

                var size = new Size(width, height);
                var pointSource = new Point(0, 0);
                var topPos = new Point(left, top);
                var blend = new BLENDFUNCTION
                {
                    BlendOp = GdiplusPInvoke.AC_SRC_OVER,
                    BlendFlags = 0,
                    SourceConstantAlpha = opacity,
                    AlphaFormat = GdiplusPInvoke.AC_SRC_ALPHA
                };

                GdiplusPInvoke.UpdateLayeredWindow(
                    handle,
                    screenDc, 
                    ref topPos,
                    ref size, 
                    memDc, 
                    ref pointSource, 
                    0, 
                    ref blend, 
                    GdiplusPInvoke.ULW_ALPHA);
            }
            finally
            {
                GdiplusPInvoke.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    GdiplusPInvoke.DeleteObject(hBitmap);
                    GdiplusPInvoke.SelectObject(memDc, oldBitmap);
                }
                GdiplusPInvoke.DeleteDC(memDc);
            }
        }
    }

    public class GdiplusException : Exception
    {
        public GdiplusException()
        {
        }

        public GdiplusException(string message) : base(message)
        {
        }

        public GdiplusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GdiplusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
