using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class TaskbarBuilder
    {
        private static IntPtr nextTaskbar;
        private static Rectangle rectangle;
        private static Screen monitor;
        private static TagRect tagRect;

        internal static IList<Taskbar> UpdateTaskbarList(this IList<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(new Taskbar(FindWindow("Shell_TrayWnd", null)));

            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (nextTaskbar == IntPtr.Zero)
                {
                    return taskbars;
                }

                taskbars.Add(new Taskbar(nextTaskbar));
            }
        }

        internal static Rectangle AdjustRect(this IntPtr handle)
        {
            // todo: have a bug here:
            GetWindowRect(handle, out tagRect);
            rectangle = tagRect;

            monitor = Screen.FromHandle(handle);
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
