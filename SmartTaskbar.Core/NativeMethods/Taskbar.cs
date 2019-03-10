using System;
using System.Drawing;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    internal class Taskbar
    {
        public IntPtr Handle { get; }

        public IntPtr Monitor { get; }

        public Rectangle Rect { get; }

        public bool IsIntersect { get; set; }

        public Taskbar(IntPtr handle)
        {
            Handle = handle;
            Monitor = handle.GetMonitor();
            GetWindowRect(handle, out lpRect);
            Rect = AdjustRect(lpRect);
        }

        private Rectangle AdjustRect(TagRect lpRect)
        {
            Rectangle rectangle = lpRect;

            var monitor = Screen.FromHandle(Handle);
            if (monitor.Bounds.Bottom < rectangle.Bottom)
            {
                rectangle.Offset(0, monitor.Bounds.Bottom - rectangle.Bottom);
                return rectangle;
            }

            if (monitor.Bounds.Top > rectangle.Top)
            {
                rectangle.Offset(0, monitor.Bounds.Top - rectangle.Top);
                return rectangle;
            }

            if (monitor.Bounds.Left > rectangle.Left)
            {
                rectangle.Offset(monitor.Bounds.Left - rectangle.Left, 0);
                return rectangle;
            } 
            if (monitor.Bounds.Right < rectangle.Right)
            {
                rectangle.Offset(monitor.Bounds.Right - rectangle.Right, 0);
                return rectangle;
            }

            return rectangle;
        }
    }
}
