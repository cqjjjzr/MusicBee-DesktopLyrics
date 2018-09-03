using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Unmanaged
{
    public static class UnmanagedHelper
    {
        public static void SetTopMost(Form form)
        {
            // new IntPtr(-1) = HWND_TOPMOST
            if (!Unmanaged.SetWindowPos(form.Handle, new IntPtr(-1), 0, 0, 0, 0,
                Unmanaged.SetWindowPosFlags.IgnoreMove | Unmanaged.SetWindowPosFlags.IgnoreResize))
                throw new UnmanagedException($"Failed executing SetWindowPos, last error {Marshal.GetLastWin32Error()}");
        }
    }

    public static class Unmanaged
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            AsynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            DoNotActivate = 0x0010,
            DoNotCopyBits = 0x0100,
            IgnoreMove = 0x0002,
            DoNotChangeOwnerZOrder = 0x0200,
            DoNotRedraw = 0x0008,
            DoNotReposition = 0x0200,
            DoNotSendChangingEvent = 0x0400,
            IgnoreResize = 0x0001,
            IgnoreZOrder = 0x0004,
            ShowWindow = 0x0040,
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        internal int left;
        internal int top;
        internal int right;
        internal int bottom;
    }

    public enum Bool
    {
        False = 0,
        True
    };

    public class UnmanagedException : Exception
    {
        public UnmanagedException()
        {
        }

        public UnmanagedException(string message) : base(message)
        {
        }

        public UnmanagedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnmanagedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
