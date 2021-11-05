namespace SmartTaskbar;

using static SafeNativeMethods;

internal static class TaskbarHelper
{
    #region Initialize the taskbar Info

    /// <summary>
    ///     Main Taskbar Class Name
    /// </summary>
    private const string MainTaskbarClassName = "Shell_TrayWnd";

    internal static TaskbarInfo InitTaskbar()
    {
        // Find the main taskbar handle
        var handle = FindWindow(MainTaskbarClassName, null);

        // Get taskbar window rectangle
        _ = GetWindowRect(handle, out var rect);

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
    internal static void HideTaskbar(this TaskbarInfo taskbar)
    {
        if (taskbar.IsShow)
            // Send a message to hide the taskbar, if taskbar is display
            PostMessage(taskbar.Handle,
                        BarFlag,
                        IntPtr.Zero,
                        IntPtr.Zero);
    }

    /// <summary>
    ///     Show the taskbar, in auto-hide mode
    /// </summary>
    /// <param name="taskbar"></param>
    /// <param name="monitor"></param>
    internal static void ShowTaskar(this TaskbarInfo taskbar)
    {
        // Send a message to show the taskbar, if taskbar is hidden
        if (!taskbar.IsShow)
            PostMessage(
                taskbar.Handle,
                BarFlag,
                (IntPtr) 1,
                MonitorFromPoint(PointZero, MonitorDefaultToPrimary));
    }

    #endregion

    #region Determine whether it need to display the taskbar

    private const uint GaParent = 1;
    private const int Tolerance = 20;

    private const string Progman = "Progman";
    private const string WorkerW = "WorkerW";
    private const string TaskListThumbnailWnd = "TaskListThumbnailWnd";

    /// <summary>
    ///     Mouse over the taskbar or a specific window,
    ///     it will only cause the taskbar to show or do nothing.
    /// </summary>
    /// <param name="taskbar"></param>
    /// <param name="mouseOverHandle"></param>
    /// <returns></returns>
    internal static TaskbarBehavior ShouldMouseOverWindowShowTheTaskbar(this TaskbarInfo taskbar)
    {
        // Get mouse coordinates
        _ = GetCursorPos(out var point);

        // use the point to get the window below it
        // this method is the fastest
        var mouseOverHandle = WindowFromPoint(point);

        // If the current handle is the taskbar, return directly.
        if (taskbar.Handle == mouseOverHandle)
            return TaskbarBehavior.Pending;

        // Not sure if this will happen.
        if (mouseOverHandle.IsWindowInvisible())
            return TaskbarBehavior.Pending;

        // Traverse to get the parent of the current window.
        // If its parent is the taskbar, it means that the mouse is on the taskbar.
        // Otherwise, all the way to the highest level, the desktop, jump out of the loop.
        var desktopHandle = GetDesktopWindow();
        var tempHandle = mouseOverHandle;
        do
        {
            tempHandle = GetAncestor(tempHandle, GaParent);

            if (taskbar.Handle == tempHandle)
                return TaskbarBehavior.DoNothing;
        }
        while (tempHandle != desktopHandle);

        // Some third-party software will parasitic on the taskbar
        // in order to prevent hide the taskbar by misjudgment.
        // Skip the windows that satisfy top and bottom in the range.
        _ = GetWindowRect(mouseOverHandle, out var mouseOverRect);

        if (mouseOverRect.top >= taskbar.Rect.top - Tolerance
            && mouseOverRect.bottom <= taskbar.Rect.bottom + Tolerance
            && mouseOverRect.left >= taskbar.Rect.left - Tolerance
            && mouseOverRect.right <= taskbar.Rect.right + Tolerance)
            return TaskbarBehavior.DoNothing;

        // If it is a thumbnail of the floating taskbar icon,
        // the taskbar needs to be displayed.

        switch (mouseOverHandle.GetName())
        {
            case TaskListThumbnailWnd:
                return TaskbarBehavior.Show;
        }

        return TaskbarBehavior.Pending;
    }

    internal static TaskbarBehavior ShouldForegroundWindowShowTheTaskbar(this TaskbarInfo taskbar)
    {
        var foregroundHandle = GetForegroundWindow();

        //When the system is start up or a window is closed,
        //there is a certain probability that the taskbar will be set to foreground window.
        // In this case, if you do not manually set the taskbar display,
        // the taskbar will not be displayed.
        if (foregroundHandle == taskbar.Handle)
            return TaskbarBehavior.Show;

        // Somehow, the foreground window is not necessarily visible.
        if (foregroundHandle.IsWindowInvisible())
            return TaskbarBehavior.Pending;

        switch (foregroundHandle.GetName())
        {
            // Determine whether it is a desktop.
            case Progman:
            case WorkerW:
                return TaskbarBehavior.Show;
        }

        // Get foreground window Rectange
        _ = GetWindowRect(foregroundHandle, out var rect);

        // if it intersects, the taskbar will be hidden.
        return rect.bottom > taskbar.Rect.top
               && rect.top < taskbar.Rect.bottom
               && rect.left < taskbar.Rect.right
               && rect.right > taskbar.Rect.left
            ? TaskbarBehavior.Hide
            : TaskbarBehavior.Show;
    }

    private static bool _enumWindowResult;
    private static TaskbarInfo _taskbarInfo;

    internal static TaskbarBehavior ShouldVisibleWindowShowTheTaskbar(this in TaskbarInfo taskbar)
    {
        _enumWindowResult = false;
        _taskbarInfo = taskbar;
        EnumWindows((handle, _) =>
                    {
                        if (handle.IsWindowInvisible()) return true;

                        GetWindowRect(handle, out var rect);

                        if (rect.bottom <= _taskbarInfo.Rect.top
                            || rect.top >= _taskbarInfo.Rect.bottom
                            || rect.left >= _taskbarInfo.Rect.right
                            || rect.right <= _taskbarInfo.Rect.left) return true;

                        switch (handle.GetName())
                        {
                            case Progman:
                            case WorkerW:
                            case MainTaskbarClassName:
                                return true;
                        }

                        _enumWindowResult = true;
                        return false;

                    },
                    IntPtr.Zero);

        return _enumWindowResult ? TaskbarBehavior.Hide : TaskbarBehavior.Show;
    }

    #endregion
}
