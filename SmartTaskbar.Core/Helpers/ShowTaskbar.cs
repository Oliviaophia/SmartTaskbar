using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ShowTaskbar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PostMessageHideTaskbar() =>
            PostMessage(FindWindow("Shell_TrayWnd", null), Constant.TaskabrFlag, IntPtr.Zero, IntPtr.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PostMesssageShowTaskbar(this IntPtr handle) =>
            PostMessage(FindWindow("Shell_TrayWnd", null), Constant.TaskabrFlag, (IntPtr) 1, handle);
    }
}