using System;
using System.Drawing;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class MonitorHelper
    {
        private const int MonitorDefaulttonearest = 2;

        internal static IntPtr GetMonitor(this IntPtr handle)
            => MonitorFromWindow(handle, MonitorDefaulttonearest);


        // MonitorFromPoint will be faster than MonitorFromRect
        internal static IntPtr GetMonitor(this Rectangle rectangle)
            => MonitorFromPoint(new TagPoint {x = rectangle.Left, y = rectangle.Top}, MonitorDefaulttonearest);
    }
}
