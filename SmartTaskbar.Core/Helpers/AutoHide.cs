using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{

    internal static class AutoHide
    {
        internal static APPBARDATA msgData = new APPBARDATA { cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA)) };
        private const int ABS_AUTOHIDE = 1;
        private const int ABS_ALWAYSONTOP = 2;
        private const uint ABM_SETSTATE = 10;
        private const uint ABM_GETSTATE = 4;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetAutoHide()
        {
            msgData.lParam = ABS_AUTOHIDE;
            SHAppBarMessage(ABM_SETSTATE, ref msgData);
            ShowTaskbar.PostMessageHideTaskbar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CancelAutoHide()
        {
            msgData.lParam = ABS_ALWAYSONTOP;
            SHAppBarMessage(ABM_SETSTATE, ref msgData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsAutoHide() => SHAppBarMessage(ABM_GETSTATE, ref msgData) == (IntPtr)1;
    }
}
