using System;
using System.Collections.Generic;
using System.Linq;

using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class MouseHover
    {
        private static IntPtr lastHandle;
        private static IntPtr currentHandle;
        private static IntPtr desktopHandle;
        private static IntPtr taskbar;
        private static IntPtr monitor;
        private static TagPoint point;
        private static bool lastResult;

        internal static bool IsMouseOverTaskbar(this IList<Taskbar> taskbars)
        {
            GetCursorPos(out point);
            currentHandle = WindowFromPoint(point);
            if (lastHandle == currentHandle)
            {
                return lastResult;
            }

            monitor = currentHandle.GetMonitor();
            taskbar = taskbars.Where(_ => _.Monitor == monitor).Select(_ => _.Handle).FirstOrDefault();
            if (taskbar == IntPtr.Zero)
            {
                return lastResult = false;
            }

            lastHandle = currentHandle;
            desktopHandle = GetDesktopWindow();
            while (currentHandle != desktopHandle)
            {
                if (taskbar == currentHandle)
                {
                    return lastResult = true;
                }

                currentHandle = currentHandle.GetParentWindow();
            }

            return lastResult = false;
        }
    }
}
