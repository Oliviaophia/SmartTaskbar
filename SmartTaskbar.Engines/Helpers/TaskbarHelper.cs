using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SmartTaskbar.Models;
using static SmartTaskbar.PlatformInvoke.SafeNativeMethods;

namespace SmartTaskbar.Engines.Helpers
{
    internal static class TaskbarHelper
    {
        private const int WmThemeChanged = 0x031A;
        private const int SwHide = 0;
        private const int SwShow = 1;

        private static IntPtr _accentPtr;
        private static WindowCompositionAttributeData _data;

        /// <summary>
        ///     Too complicated to explain, so no comments
        /// </summary>
        /// <param name="taskbars"></param>
        /// <param name="taskbarState"></param>
        internal static void SetBarState(this List<Taskbar> taskbars, TaskbarState taskbarState)
        {
            if (taskbarState.HideTaskbarCompletely)
            {
                AutoHideHelper.SetAutoHide(true);
                foreach (var taskbar in taskbars) _ = ShowWindow(taskbar.Handle, SwHide);
            }
            else
            {
                if (taskbarState.TransparentMode == TransparentModeType.Disable)
                {
                    ButtonSizeHelper.SetIconSize(taskbarState.IconSize);
                    AutoHideHelper.SetAutoHide(taskbarState.IsAutoHide);

                    foreach (var taskbar in taskbars) _ = ShowWindow(taskbar.Handle, SwShow);

                    PostMessage(FindWindow(Constants.MainTaskbar, null), WmThemeChanged, IntPtr.Zero, IntPtr.Zero);

                    if (_accentPtr == IntPtr.Zero) return;

                    Marshal.FreeHGlobal(_accentPtr);
                    _accentPtr = IntPtr.Zero;
                }
                else
                {
                    if (_accentPtr == IntPtr.Zero)
                    {
                        var size = Marshal.SizeOf(typeof(AccentPolicy));
                        _accentPtr = Marshal.AllocHGlobal(size);
                        _data = new WindowCompositionAttributeData
                        {
                            Attribute = 19,
                            SizeOfData = size,
                            Data = _accentPtr
                        };
                    }

                    AccentPolicy accent;
                    switch (taskbarState.TransparentMode)
                    {
                        case TransparentModeType.Transparent:
                            accent = new AccentPolicy
                            {
                                AccentState = 3,
                                AccentFlags = 1
                            };
                            break;
                        case TransparentModeType.Blur:
                            accent = new AccentPolicy
                            {
                                AccentState = 2,
                                AccentFlags = 2,
                                GradientColor = 0
                            };
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    Marshal.StructureToPtr(accent, _accentPtr, false);

                    ButtonSizeHelper.SetIconSize(taskbarState.IconSize);
                    AutoHideHelper.SetAutoHide(taskbarState.IsAutoHide);
                    foreach (var taskbar in taskbars)
                    {
                        _ = SetWindowCompositionAttribute(taskbar.Handle, ref _data);

                        _ = ShowWindow(taskbar.Handle, SwShow);
                    }
                }
            }
        }

        internal static void MaintainBarState(this List<Taskbar> taskbars, TaskbarState taskbarState)
        {
            if (taskbarState.HideTaskbarCompletely)
            {
                foreach (var taskbar in taskbars) _ = ShowWindow(taskbar.Handle, SwHide);
            }
            else
            {
                if (taskbarState.TransparentMode == TransparentModeType.Disable) return;

                foreach (var taskbar in taskbars) _ = SetWindowCompositionAttribute(taskbar.Handle, ref _data);
            }
        }

        internal static List<Taskbar> ResetTaskbars(this List<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(FindWindow(Constants.MainTaskbar, null).InitTaskbar());

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, Constants.SubTaskbar, "");
                if (nextTaskbar == IntPtr.Zero) return taskbars;

                taskbars.Add(nextTaskbar.InitTaskbar());
            }
        }

        private static Taskbar InitTaskbar(this IntPtr handle)
        {
            GetWindowRect(handle, out var tagRect);
            Rectangle rectangle = tagRect;
            var monitor = Screen.FromHandle(handle);

            if (rectangle.Width > rectangle.Height)
            {
                // this taskbar is either on the top or bottom:
                var heightΔ = monitor.Bounds.Bottom - rectangle.Bottom;

                // this taskbar is on the bottom of this monitor:
                if (heightΔ == 0) return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

                // this taskbar is on the bottom of this monitor (hide):
                if (heightΔ == 2 - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // this taskbar is on the top of the below monitor (hide):
                if (heightΔ == -2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // this taskbar is on the top of this monitor:
                if (heightΔ == monitor.Bounds.Height - rectangle.Height)
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

                // this taskbar is on the top of this monitor (hide):
                if (heightΔ == monitor.Bounds.Height - 2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // this taskbar is on the bottom of the above monitor (hide):
                if (heightΔ == 2 + monitor.Bounds.Height - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // This may be triggered when switching the display monitor
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is either on the left or right:
            var widthΔ = rectangle.Left - monitor.Bounds.Left;

            // this taskbar is on the left of this monitor:
            if (widthΔ == 0) return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

            // this taskbar is on the left of this monitor (hide):
            if (widthΔ == 2 - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is on the right of the left side monitor (hide):
            if (widthΔ == -2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is on the right of this monitor:
            if (widthΔ == monitor.Bounds.Width - rectangle.Width)
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

            // this taskbar is on the right of this monitor (hide):
            if (widthΔ == monitor.Bounds.Width - 2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is on the left of the right side monitor (hide):
            if (widthΔ == 2 + monitor.Bounds.Width - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // This may be triggered when switching the display monitor
            return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
        }
    }
}
