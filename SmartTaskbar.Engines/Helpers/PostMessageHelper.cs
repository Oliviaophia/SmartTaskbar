using System;
using SmartTaskbar.Models;
using static SmartTaskbar.PlatformInvoke.SafeNativeMethods;

namespace SmartTaskbar.Engines.Helpers
{
    internal static class PostMessageHelper
    {
        private const uint BarFlag = 0x05D1;


        internal static void HideTaskbar()
            => PostMessage(FindWindow(Constants.MainTaskbar, null),
                           BarFlag,
                           IntPtr.Zero,
                           IntPtr.Zero);


        internal static void ShowTaskar(this IntPtr handle)
            => PostMessage(
                FindWindow(Constants.MainTaskbar, null),
                BarFlag,
                (IntPtr) 1,
                handle);
    }
}
