namespace SmartTaskbar;

internal class Taskbar
{
    public Taskbar(IntPtr handle, IntPtr monitor, Rectangle rectangle, bool intersect)
    {
        Handle = handle;
        Monitor = monitor;
        Rectangle = rectangle;
        Intersect = intersect;
    }

    public IntPtr Handle { get; init; }

    public IntPtr Monitor { get; init; }

    public Rectangle Rectangle { get; set; }

    public bool Intersect { get; set; }
}
