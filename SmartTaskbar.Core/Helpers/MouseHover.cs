using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private static POINT point;
        private static bool lastResult;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
