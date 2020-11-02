using System;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ShowBar
    {
        private const uint BarFlag = 0x05D1;

        internal static void PostMessageHideBar()
            => PostMessage(FindWindow(Constant.MainTaskbar, null),
                           BarFlag,
                           IntPtr.Zero,
                           IntPtr.Zero);

        internal static void PostMesssageShowBar(this IntPtr handle)
            => PostMessage(
                FindWindow(Constant.MainTaskbar, null),
                BarFlag,
                (IntPtr) 1,
                handle);
    }
}
