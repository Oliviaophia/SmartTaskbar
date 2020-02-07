using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class BarBuilder
    {
        // Do not use Higher order function or Lambda here; Advanced syntax is not used here for performance reasons;

        internal static List<Taskbar> ResetTaskbars(this List<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(FindWindow("Shell_TrayWnd", null).InitTaskbar());

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (nextTaskbar == IntPtr.Zero) return taskbars;

                taskbars.Add(nextTaskbar.InitTaskbar());
            }
        }

        private static Taskbar InitTaskbar(this IntPtr handle)
        {
            var taskbar = new Taskbar(handle);
            GetWindowRect(handle, out var tagRect);
            Rectangle rectangle = tagRect;
            var monitor = Screen.FromHandle(handle);

            if (rectangle.Width > rectangle.Height)
            {
                // this taskbar is either on the top or bottom:
                var heightΔ = monitor.Bounds.Bottom - rectangle.Bottom;

                // this taskbar is on the bottom of this monitor:
                if (heightΔ == 0)
                {
                    taskbar.Monitor = handle.GetMonitor();
                    taskbar.Rect = rectangle;
                    return taskbar;
                }

                // this taskbar is on the bottom of this monitor (hide):
                if (heightΔ == 2 - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    taskbar.Monitor = handle.GetMonitor();
                    taskbar.Rect = rectangle;
                    return taskbar;
                }

                // this taskbar is on the top of the below monitor (hide):
                if (heightΔ == -2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    taskbar.Monitor = rectangle.GetMonitor();
                    taskbar.Rect = rectangle;
                    return taskbar;
                }

                // this taskbar is on the top of this monitor:
                if (heightΔ == monitor.Bounds.Height - rectangle.Height)
                {
                    taskbar.Monitor = handle.GetMonitor();
                    taskbar.Rect = rectangle;
                    return taskbar;
                }

                // this taskbar is on the top of this monitor (hide):
                if (heightΔ == monitor.Bounds.Height - 2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    taskbar.Monitor = handle.GetMonitor();
                    taskbar.Rect = rectangle;
                    return taskbar;
                }

                // this taskbar is on the bottom of the above monitor (hide):
                if (heightΔ == 2 + monitor.Bounds.Height - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    taskbar.Monitor = rectangle.GetMonitor();
                    taskbar.Rect = rectangle;
                    return taskbar;
                }

                // This may be triggered when switching the display monitor
                taskbar.Monitor = handle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // this taskbar is either on the left or right:
            var widthΔ = rectangle.Left - monitor.Bounds.Left;

            // this taskbar is on the left of this monitor:
            if (widthΔ == 0)
            {
                taskbar.Monitor = handle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // this taskbar is on the left of this monitor (hide):
            if (widthΔ == 2 - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                taskbar.Monitor = handle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // this taskbar is on the right of the left side monitor (hide):
            if (widthΔ == -2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                taskbar.Monitor = rectangle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // this taskbar is on the right of this monitor:
            if (widthΔ == monitor.Bounds.Width - rectangle.Width)
            {
                taskbar.Monitor = handle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // this taskbar is on the right of this monitor (hide):
            if (widthΔ == monitor.Bounds.Width - 2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                taskbar.Monitor = handle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // this taskbar is on the left of the right side monitor (hide):
            if (widthΔ == 2 + monitor.Bounds.Width - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                taskbar.Monitor = rectangle.GetMonitor();
                taskbar.Rect = rectangle;
                return taskbar;
            }

            // This may be triggered when switching the display monitor
            taskbar.Monitor = handle.GetMonitor();
            taskbar.Rect = rectangle;
            return taskbar;
        }
    }
}