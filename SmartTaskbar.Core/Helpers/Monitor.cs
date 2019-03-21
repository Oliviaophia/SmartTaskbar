using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class Monitor
    {
        private const int MonitorDefaulttonearest = 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IntPtr GetMonitor(this IntPtr handle) => MonitorFromWindow(handle, MonitorDefaulttonearest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IntPtr GetMonitor(this TagPoint point) => MonitorFromPoint(point, MonitorDefaulttonearest);
    }
}