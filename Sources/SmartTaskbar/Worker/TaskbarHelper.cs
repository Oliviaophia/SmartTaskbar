namespace SmartTaskbar;

using static Fun;

public static class TaskbarHelper

{
    #region Initialize the taskbar Info

    /// <summary>
    ///     Main Taskbar Class Name
    /// </summary>
    private const string MainTaskbarClassName = "Shell_TrayWnd";

    public static TaskbarInfo InitTaskbar()
    {
        // Find the main taskbar handle
        var handle = FindWindow(MainTaskbarClassName, null);

        if (handle == IntPtr.Zero)
            throw new ApplicationException("The SmartTaskbar is unable to get the handle of the taskbar.");

        // Get taskbar window rectangle
        if (!GetWindowRect(handle, out var rect))
            throw new ApplicationException("The SmartTaskbar is unable to get the rectangle of the taskbar.");

        // Currently, the taskbar of Windows 11 is only at the bottom,
        // so you only need to calculate the difference between the taskbar and the bottom of the screen
        // to get the rectangle when the taskbar is fully displayed.
        var heightΔ = rect.bottom - Screen.PrimaryScreen.Bounds.Bottom;

        return new TaskbarInfo(handle,
                               new TagRect
                               {
                                   left = rect.left,
                                   top = rect.top - heightΔ,
                                   right = rect.right,
                                   bottom = rect.bottom - heightΔ
                               },
                               heightΔ == 0);
    }

    #endregion

    #region Show Or Hide Taskbar

    private const uint BarFlag = 0x05D1;

    private const uint MonitorDefaultToPrimary = 1;
    private static readonly TagPoint PointZero = new() {x = 0, y = 0};

    /// <summary>
    ///     Hide the taskbar, in auto-hide mode
    /// </summary>
    /// <param name="taskbar"></param>
    public static void HideTaskbar(this in TaskbarInfo taskbar)
    {
        if (taskbar.IsShow)
            // Send a message to hide the taskbar, if taskbar is display
            _ = PostMessage(taskbar.Handle,
                            BarFlag,
                            IntPtr.Zero,
                            IntPtr.Zero);
    }

    /// <summary>
    ///     Show the taskbar, in auto-hide mode
    /// </summary>
    /// <param name="taskbar"></param>
    public static void ShowTaskar(this in TaskbarInfo taskbar)
    {
        // Send a message to show the taskbar, if taskbar is hidden
        if (!taskbar.IsShow)
            _ = PostMessage(
                taskbar.Handle,
                BarFlag,
                (IntPtr) 1,
                MonitorFromPoint(PointZero, MonitorDefaultToPrimary));
    }

    #endregion

    #region Determine whether it need to display the taskbar

    private const uint GaRoot = 2;
    private const int Tolerance = 20;

    private const string Progman = "Progman";
    private const string WorkerW = "WorkerW";
    private const string TaskListThumbnailWnd = "TaskListThumbnailWnd";

    /// <summary>
    ///     Mouse over the taskbar or a specific window,
    ///     it will only cause the taskbar to show or do nothing.
    /// </summary>
    /// <param name="taskbar"></param>
    /// <returns></returns>
    public static TaskbarBehavior ShouldMouseOverWindowShowTheTaskbar(this in TaskbarInfo taskbar)
    {
        // Get mouse coordinates
        if (!GetCursorPos(out var point))
            return TaskbarBehavior.Pending;

        // use the point to get the window below it
        // this method is the fastest
        var mouseOverHandle = WindowFromPoint(point);

        // WindowFromPoint unable to get the correct window
        if (mouseOverHandle == IntPtr.Zero)
            return TaskbarBehavior.Pending;

        // If the current handle is the taskbar, return directly.
        if (taskbar.Handle == mouseOverHandle)
            return TaskbarBehavior.DoNothing;

        // If the current handle is within the taskbar, return directly.
        if (taskbar.Handle == GetAncestor(mouseOverHandle, GaRoot))
            return TaskbarBehavior.DoNothing;

        // Some third-party software will parasitic on the taskbar
        // in order to prevent hide the taskbar by misjudgment.
        // Skip the windows that satisfy top and bottom in the range.
        if (GetWindowRect(mouseOverHandle, out var mouseOverRect)
            && mouseOverRect.top >= taskbar.Rect.top - Tolerance
            && mouseOverRect.bottom <= taskbar.Rect.bottom + Tolerance
            && mouseOverRect.left >= taskbar.Rect.left - Tolerance
            && mouseOverRect.right <= taskbar.Rect.right + Tolerance)
            return TaskbarBehavior.DoNothing;

        // If it is a thumbnail of the floating taskbar icon,
        // the taskbar needs to be displayed.
        return mouseOverHandle.GetName() == TaskListThumbnailWnd
            ? TaskbarBehavior.Show
            : TaskbarBehavior.Pending;
    }

    public static TaskbarBehavior ShouldForegroundWindowShowTheTaskbar(this TaskbarInfo taskbar)
    {
        var foregroundHandle = GetForegroundWindow();

        if (foregroundHandle == IntPtr.Zero)
            return TaskbarBehavior.Pending;

        // When the system is start up or a window is closed,
        // there is a certain probability that the taskbar will be set to foreground window.
        if (foregroundHandle == taskbar.Handle)
            return TaskbarBehavior.Show;

        // Somehow, the foreground window is not necessarily visible.
        if (foregroundHandle.IsWindowInvisible())
            return TaskbarBehavior.Pending;

        // Get foreground window Rectange.
        if (!GetWindowRect(foregroundHandle, out var rect))
            return TaskbarBehavior.Pending;

        // If the window and the taskbar do not intersect, the taskbar should be displayed.
        if (rect.bottom <= taskbar.Rect.top
            || rect.top >= taskbar.Rect.bottom
            || rect.left >= taskbar.Rect.right
            || rect.right <= taskbar.Rect.left)
            return TaskbarBehavior.Show;

        // Unless it's a desktop.
        return foregroundHandle.GetName() is Progman or WorkerW
            ? TaskbarBehavior.Show
            : TaskbarBehavior.Hide;
    }

    public static TaskbarBehavior ShouldDesktopShowTheTaskbar(this in TaskbarInfo taskbar)
    {
        // Take a point on the taskbar to determine whether its current window is the desktop,
        // if it is, the taskbar should be displayed
        var window = WindowFromPoint(new TagPoint {x = taskbar.Rect.left, y = taskbar.Rect.top});

        if (window == IntPtr.Zero)
            return TaskbarBehavior.Pending;

        if (window == taskbar.Handle)
            return TaskbarBehavior.Pending;

        var rootWindow = GetAncestor(window, GaRoot);

        if (rootWindow == taskbar.Handle)
            return TaskbarBehavior.Pending;

        var name = rootWindow.GetName();

        return name is Progman or WorkerW ? TaskbarBehavior.Show : TaskbarBehavior.Pending;
    }

    #endregion
}
