namespace SmartTaskbar;

using static SafeNativeMethods;

internal readonly struct TaskbarInfo
{
    public TaskbarInfo(IntPtr handle, TagRect rect)
    {
        Handle = handle;
        Rect = rect;
    }

    public readonly IntPtr Handle;

    public readonly TagRect Rect;
}
