using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ShowBar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PostMessageHideBar() =>
            PostMessage(FindWindow("Shell_TrayWnd", null), Constant.BarFlag, IntPtr.Zero, IntPtr.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PostMesssageShowBar(this IntPtr handle) =>
            PostMessage(FindWindow("Shell_TrayWnd", null), Constant.BarFlag, (IntPtr) 1, handle);
    }
}