namespace SmartTaskbar;

using static SafeNativeMethods;

internal readonly struct TaskbarInfo
{
    public TaskbarInfo(IntPtr taskbarHandle, IntPtr monitorHandle, TagRect taskbarRectangle, Rectangle monitorRectangle)
    {
        TaskbarHandle = taskbarHandle;
        MonitorHandle = monitorHandle;
        TaskbarRectangle = taskbarRectangle;
        MonitorRectangle = monitorRectangle;
    }

    public readonly IntPtr TaskbarHandle;

    public readonly IntPtr MonitorHandle;

    public readonly TagRect TaskbarRectangle;

    public readonly Rectangle MonitorRectangle;
}
