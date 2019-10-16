using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class Transparent
    {
        // Broadcast to every window following a theme change event.
        // Examples of theme change events are the activation of a theme,
        // the deactivation of a theme, or a transition from one theme to another.
        private const int WmThemechanged = 0x031A;
        private static IntPtr _accentPtr;
        private static WindowCompositionAttributeData _data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void TransparentBar(this List<Taskbar> taskbars, TransparentModeType modeType, bool stateChange)
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

            if (stateChange)
            {
                AccentPolicy accent;
                switch (modeType)
                {
                    case TransparentModeType.Disabled:
                        PostMessage(IntPtr.Zero, WmThemechanged, IntPtr.Zero, IntPtr.Zero);
                        Free();
                        return;
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
            }

            foreach (var taskbar in taskbars) SetWindowCompositionAttribute(taskbar.Handle, ref _data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Free()
        {
            Marshal.FreeHGlobal(_accentPtr);
            _accentPtr = IntPtr.Zero;
        }
    }
}