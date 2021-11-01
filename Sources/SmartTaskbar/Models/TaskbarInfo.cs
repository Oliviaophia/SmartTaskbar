namespace SmartTaskbar;
using static SmartTaskbar.SafeNativeMethods;

internal struct TaskbarInfo
{
    public TaskbarInfo(IntPtr taskbarHandle, IntPtr monitorHandle, TagRect taskbarRectangle, TagRect monitorRectangle)
    {
        TaskbarHandle = taskbarHandle;
        MonitorHandle = monitorHandle;
        MonitorRectangle = monitorRectangle;
        TaskbarRectangle = taskbarRectangle;
    }

    public IntPtr TaskbarHandle;

    public IntPtr MonitorHandle;

    public TagRect TaskbarRectangle;

    public TagRect MonitorRectangle;
}
