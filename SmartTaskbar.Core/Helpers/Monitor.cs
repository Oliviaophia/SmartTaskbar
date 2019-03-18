using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class Monitor
    {
        private const int MONITOR_DEFAULTTONEAREST = 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IntPtr GetMonitor(this IntPtr handle) => MonitorFromWindow(handle, MONITOR_DEFAULTTONEAREST);
    }
}
