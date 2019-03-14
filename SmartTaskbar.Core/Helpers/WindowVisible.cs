using System;

using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class WindowVisible
    {
        private static bool cloaked;
        private const int DWMWA_CLOAKED = 14;

        internal static bool IsWindowInvisible(this IntPtr handle)
        {
            if (IsWindowVisible(handle) == false)
            {
                return true;
            }

            DwmGetWindowAttribute(handle, DWMWA_CLOAKED, out cloaked, sizeof(int));
            return cloaked;
        }
    }
}
