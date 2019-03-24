using System;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    internal class Taskbar
    {
        public Taskbar(IntPtr handle)
        {
            Handle = handle;
            GetWindowRect(handle, out TagRect tagRect);
            Rectangle rectangle = tagRect;
            var monitor = Screen.FromHandle(handle);

            if (rectangle.Width > rectangle.Height)
            {
                // this taskbar is either on the top or bottom:
                var heightΔ = monitor.Bounds.Bottom - rectangle.Bottom;

                // this taskbar is on the bottom of this monitor:
                if (heightΔ == 0)
                {
                    Monitor = handle.GetMonitor();
                    Rect = rectangle;
                    return;
                }

                // this taskbar is on the bottom of this monitor (hide):
                if (heightΔ == 2 - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    Monitor = handle.GetMonitor();
                    Rect = rectangle;
                    return;
                }

                // this taskbar is on the top of the below monitor (hide):
                if (heightΔ == -2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    Monitor = rectangle.GetMonitor();
                    Rect = rectangle;
                    return;
                }

                // this taskbar is on the top of this monitor:
                if (heightΔ == monitor.Bounds.Height - rectangle.Height)
                {
                    Monitor = handle.GetMonitor();
                    Rect = rectangle;
                    return;
                }

                // this taskbar is on the top of this monitor (hide):
                if (heightΔ == monitor.Bounds.Height - 2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    Monitor = handle.GetMonitor();
                    Rect = rectangle;
                    return;
                }

                // this taskbar is on the bottom of the above monitor (hide):
                if (heightΔ == 2 + monitor.Bounds.Height - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    Monitor = rectangle.GetMonitor();
                    Rect = rectangle;
                    return;
                }

                // This may be triggered when switching the display monitor
                Monitor = handle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // this taskbar is either on the left or right:
            var widthΔ = rectangle.Left - monitor.Bounds.Left;

            // this taskbar is on the left of this monitor:
            if (widthΔ == 0)
            {
                Monitor = handle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // this taskbar is on the left of this monitor (hide):
            if (widthΔ == 2 - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                Monitor = handle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // this taskbar is on the right of the left side monitor (hide):
            if (widthΔ == -2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                Monitor = rectangle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // this taskbar is on the right of this monitor:
            if (widthΔ == monitor.Bounds.Width - rectangle.Width)
            {
                Monitor = handle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // this taskbar is on the right of this monitor (hide):
            if (widthΔ == monitor.Bounds.Width - 2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                Monitor = handle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // this taskbar is on the left of the right side monitor (hide):
            if (widthΔ == 2 + monitor.Bounds.Width - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                Monitor = rectangle.GetMonitor();
                Rect = rectangle;
                return;
            }

            // This may be triggered when switching the display monitor
            Monitor = handle.GetMonitor();
            Rect = rectangle;
        }

        public IntPtr Handle { get; }

        public IntPtr Monitor { get; }

        public Rectangle Rect { get; }

        public bool Intersect { get; set; }
    }
}