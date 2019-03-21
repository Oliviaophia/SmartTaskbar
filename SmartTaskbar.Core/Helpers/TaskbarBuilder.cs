using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class TaskbarBuilder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IList<Taskbar> UpdateTaskbarList(this IList<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(new Taskbar(FindWindow("Shell_TrayWnd", null)));

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (nextTaskbar == IntPtr.Zero) return taskbars;

                taskbars.Add(new Taskbar(nextTaskbar));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (IntPtr, IntPtr, Rectangle) SetTaskbar(this IntPtr handle)
        {
            GetWindowRect(handle, out TagRect tagRect);
            Rectangle rectangle = tagRect;
            var monitor = Screen.FromHandle(handle);
            // todo: need to clean up
            if (rectangle.Width > rectangle.Height)
            {
                // bottom
                var heightΔ = monitor.Bounds.Bottom - rectangle.Bottom;
                if (heightΔ != 0)
                {
                    if (heightΔ == -2)
                    {
                        rectangle.Offset(0, rectangle.Height + heightΔ);
                        return (
                            handle,
                            new TagPoint {x = rectangle.Left, y = rectangle.Bottom}.GetMonitor(),
                            rectangle
                        );
                    }

                    if (heightΔ < 0)
                    {
                        rectangle.Offset(0, heightΔ);
                        return (
                            handle,
                            handle.GetMonitor(),
                            rectangle
                        );
                    }

                    if (heightΔ == monitor.Bounds.Height - rectangle.Height + 2)
                    {
                        rectangle.Offset(0, 2 - rectangle.Height);
                        return (
                            handle,
                            new TagPoint {x = rectangle.Left, y = rectangle.Top}.GetMonitor(),
                            rectangle
                        );
                    }
                }

                // top
                heightΔ = monitor.Bounds.Top - rectangle.Top;
                if (heightΔ == 2)
                {
                    rectangle.Offset(0, heightΔ - rectangle.Height);
                    return (
                        handle,
                        new TagPoint {x = rectangle.Left, y = rectangle.Top}.GetMonitor(),
                        rectangle
                    );
                }

                if (heightΔ == 2 + rectangle.Height - monitor.Bounds.Height)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    return (
                        handle,
                        new TagPoint {x = rectangle.Left, y = rectangle.Bottom}.GetMonitor(),
                        rectangle
                    );
                }

                if (heightΔ > 0) rectangle.Offset(0, heightΔ);

                return (
                    handle,
                    handle.GetMonitor(),
                    rectangle
                );
            }

            // left
            var widthΔ = monitor.Bounds.Left - rectangle.Left;
            if (widthΔ != 0)
            {
                if (widthΔ == 2)
                {
                    rectangle.Offset(widthΔ - rectangle.Width, 0);
                    return (
                        handle,
                        new TagPoint {x = rectangle.Left, y = rectangle.Top}.GetMonitor(),
                        rectangle
                    );
                }

                if (widthΔ > 0)
                {
                    rectangle.Offset(widthΔ, 0);
                    return (
                        handle,
                        handle.GetMonitor(),
                        rectangle
                    );
                }

                if (widthΔ == 2 + rectangle.Width - monitor.Bounds.Width)
                {
                    rectangle.Offset(rectangle.Width - 2, 0);
                    return (
                        handle,
                        new TagPoint {x = rectangle.Right, y = rectangle.Top}.GetMonitor(),
                        rectangle
                    );
                }
            }

            // right
            widthΔ = monitor.Bounds.Right - rectangle.Right;
            if (widthΔ == -2)
            {
                rectangle.Offset(rectangle.Width + widthΔ, 0);
                return (
                    handle,
                    new TagPoint {x = rectangle.Right, y = rectangle.Top}.GetMonitor(),
                    rectangle
                );
            }

            if (widthΔ == 2 + monitor.Bounds.Width - rectangle.Width)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                return (
                    handle,
                    new TagPoint {x = rectangle.Left, y = rectangle.Top}.GetMonitor(),
                    rectangle
                );
            }

            if (widthΔ < 0) rectangle.Offset(widthΔ, 0);

            return (
                handle,
                handle.GetMonitor(),
                rectangle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IList<Taskbar> UpdateInersect(this IList<Taskbar> taskbars, Func<Taskbar, bool> func)
        {
            foreach (var taskbar in taskbars) taskbar.IsIntersect = func(taskbar);

            return taskbars;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowTaskbarbyInersect(this IList<Taskbar> taskbars)
        {
            foreach (var taskbar in taskbars)
            {
                if (taskbar.IsIntersect) continue;

                taskbar.Monitor.PostMesssageShowTaskbar();
                return;
            }

            ShowTaskbar.PostMessageHideTaskbar();
        }
    }
}