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

        private Rectangle AdjustRect(Rectangle lpRect)
        {
            var monitor = Screen.FromHandle(Handle);

            // todo

        }
    }
}
