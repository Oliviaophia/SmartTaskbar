namespace SmartTaskbar;
using static SmartTaskbar.SafeNativeMethods;

internal record TaskbarInfo(IntPtr TaskbarHandle, IntPtr MonitorHandle, TagRect TaskbarRectangle, TagRect MonitorRectangle);
