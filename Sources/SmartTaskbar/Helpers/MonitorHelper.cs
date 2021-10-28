using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class MonitorHelper
{
    private const uint MONITOR_DEFAULTTONEAREST = 2;
    private const uint MONITOR_DEFAULTTOPRIMARY = 1;
    private static readonly TagPoint PointZero = new() { x = 0, y = 0 };


    internal static IntPtr GetMonitor(this IntPtr handle)
        => MonitorFromWindow(handle, MONITOR_DEFAULTTONEAREST);


    // MonitorFromPoint will be faster than MonitorFromRect

    internal static IntPtr GetMonitor(this Rectangle rectangle)
        => MonitorFromPoint(new TagPoint { x = rectangle.Left, y = rectangle.Top }, MONITOR_DEFAULTTONEAREST);

    internal static IntPtr GetPrimaryMonitor()
        => MonitorFromPoint(PointZero, MONITOR_DEFAULTTOPRIMARY);
}
