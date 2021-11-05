namespace SmartTaskbar;

using static SafeNativeMethods;

/// <summary>
///     Taskbar information structure
/// </summary>
public readonly struct TaskbarInfo
{
    /// <summary>
    ///     Initialize taskbar information
    /// </summary>
    /// <returns></returns>
    public TaskbarInfo(IntPtr handle, TagRect rect, bool isShow)
    {
        Handle = handle;
        Rect = rect;
        IsShow = isShow;
    }

    /// <summary>
    ///     Taskbar handle
    /// </summary>
    public readonly IntPtr Handle;

    /// <summary>
    ///     Taskbar rectangle (in display state)
    /// </summary>
    public readonly TagRect Rect;

    /// <summary>
    ///     Whether the taskbar is displayed
    /// </summary>
    public readonly bool IsShow;
}
