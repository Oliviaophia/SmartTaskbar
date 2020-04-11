using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class BarState
    {
        private const int WmThemechanged = 0x031A;
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
                AutoHide.SetAutoHide(true);
                foreach (var taskbar in taskbars) ShowWindow(taskbar.Handle, SwHide);
            }
            else
            {
                if (taskbarState.TransparentMode == TransparentModeType.Disable)
                {
                    ButtonSize.SetIconSize(taskbarState.IconSize);
                    AutoHide.SetAutoHide(taskbarState.IsAutoHide);

                    foreach (var taskbar in taskbars) ShowWindow(taskbar.Handle, SwShow);

                    PostMessage(FindWindow(Constant.MainTaskbar, null), WmThemechanged, IntPtr.Zero, IntPtr.Zero);

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

                    ButtonSize.SetIconSize(taskbarState.IconSize);
                    AutoHide.SetAutoHide(taskbarState.IsAutoHide);
                    foreach (var taskbar in taskbars)
                    {
                        SetWindowCompositionAttribute(taskbar.Handle, ref _data);

                        ShowWindow(taskbar.Handle, SwShow);
                    }
                }
            }
        }

        internal static void MaintainBarState(this List<Taskbar> taskbars, TaskbarState taskbarState)
        {
            if (taskbarState.HideTaskbarCompletely)
            {
                foreach (var taskbar in taskbars) ShowWindow(taskbar.Handle, SwHide);
            }
            else
            {
                if (taskbarState.TransparentMode == TransparentModeType.Disable) return;

                foreach (var taskbar in taskbars) SetWindowCompositionAttribute(taskbar.Handle, ref _data);
            }
        }
    }
}