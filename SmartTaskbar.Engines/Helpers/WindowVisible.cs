using System;
using static SmartTaskbar.PlatformInvoke.SafeNativeMethods;

namespace SmartTaskbar.Engines.Helpers
{
    internal static class WindowVisible
    {
        private const int DwmwaCloaked = 14;
        private static bool _cloaked;


        internal static bool IsWindowInvisible(this IntPtr handle)
        {
            if (IsWindowVisible(handle) == false) return true;

            _ = DwmGetWindowAttribute(handle, DwmwaCloaked, out _cloaked, sizeof(int));
            return _cloaked;
        }
    }
}
