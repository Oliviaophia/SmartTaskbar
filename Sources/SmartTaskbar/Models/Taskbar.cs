namespace SmartTaskbar;

internal class Taskbar
{
    public Taskbar(IntPtr taskbarHandle, IntPtr monitorHandle, Rectangle taskbarRectangle, Rectangle monitorRectangle)
    {
        TaskbarHandle = taskbarHandle;
        MonitorHandle = monitorHandle;
        TaskbarRectangle = taskbarRectangle;
        MonitorRectangle = monitorRectangle;
    }

    public IntPtr TaskbarHandle { get; }

    public IntPtr MonitorHandle { get; }

    public Rectangle TaskbarRectangle { get; }

    public Rectangle MonitorRectangle { get; }

    public bool Intersect { get; set; }
}
