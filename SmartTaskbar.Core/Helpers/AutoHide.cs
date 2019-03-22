using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class AutoHide
    {
        private const int AbsAutohide = 1;
        private const int AbsAlwaysontop = 2;
        private const uint AbmSetstate = 10;
        private const uint AbmGetstate = 4;
        internal static AppbarData msgData = new AppbarData {cbSize = (uint) Marshal.SizeOf(typeof(AppbarData))};

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetAutoHide()
        {
            msgData.lParam = AbsAutohide;
            SHAppBarMessage(AbmSetstate, ref msgData);
            ShowTaskbar.PostMessageHideTaskbar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void CancelAutoHide()
        {
            msgData.lParam = AbsAlwaysontop;
            SHAppBarMessage(AbmSetstate, ref msgData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsAutoHide() => SHAppBarMessage(AbmGetstate, ref msgData) == (IntPtr) 1;
    }
}