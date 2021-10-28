using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class MonitorHelper
{
    private const uint MONITOR_DEFAULTTOPRIMARY = 1;
    private static readonly TagPoint PointZero = new() { x = 0, y = 0 };

    internal static IntPtr GetPrimaryMonitor()
        => MonitorFromPoint(PointZero, MONITOR_DEFAULTTOPRIMARY);
}
