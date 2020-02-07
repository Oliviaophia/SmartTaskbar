using System;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ShowBar
    {
        internal static void PostMessageHideBar() => PostMessage(FindWindow("Shell_TrayWnd", null), Constant.BarFlag,
            IntPtr.Zero, IntPtr.Zero);

        internal static void PostMesssageShowBar(this IntPtr handle) => PostMessage(FindWindow("Shell_TrayWnd", null),
            Constant.BarFlag, (IntPtr) 1, handle);
    }
}