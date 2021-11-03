namespace SmartTaskbar;

using static SafeNativeMethods;

/// <summary>
///     Taskbar information structure
/// </summary>
internal readonly struct TaskbarInfo
{
    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="rect"></param>
    public TaskbarInfo(IntPtr handle, TagRect rect)
    {
        Handle = handle;
        Rect = rect;
    }

    /// <summary>
    ///     Taskbar handle
    /// </summary>
    public readonly IntPtr Handle;

    /// <summary>
    ///     Taskbar rectangle (in display state)
    /// </summary>
    public readonly TagRect Rect;
}
