namespace SmartTaskbar
{
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

            // unable to get the handle of the taskbar.
            if (handle == IntPtr.Zero)
                return TaskbarInfo.Empty;

            // Get taskbar window rectangle
            if (!GetWindowRect(handle, out var rect))
                // unable to get the rectangle of the taskbar.
                return TaskbarInfo.Empty;

            // determine the taskbar position

            var monitor = MonitorFromPoint(PointZero, TrayMonitorDefaulttoprimary);

            // todo: The main taskbar is not always on the Primary desktop!!! When this happens, the program will run abnormally

            if (rect.right - rect.left == Screen.PrimaryScreen.Bounds.Width)
            {
                var bottomΔ = rect.bottom - Screen.PrimaryScreen.Bounds.Bottom;
                // taskbar on the top or bottom
                if (bottomΔ == 0)
                    return new TaskbarInfo(handle,
                                           new TagRect
                                           {
                                               left = rect.left,
                                               top = rect.top,
                                               right = rect.right,
                                               bottom = rect.bottom
                                           },
                                           true,
                                           TaskbarPosition.Bottom,
                                           monitor);

                if (bottomΔ > 0)
                    return new TaskbarInfo(handle,
                                           new TagRect
                                           {
                                               left = rect.left,
                                               top = rect.top - bottomΔ,
                                               right = rect.right,
                                               bottom = rect.bottom - bottomΔ
                                           },
                                           false,
                                           TaskbarPosition.Bottom,
                                           monitor);

                var topΔ = rect.top - Screen.PrimaryScreen.Bounds.Top;
                return new TaskbarInfo(handle,
                                       new TagRect
                                       {
                                           left = rect.left,
                                           top = rect.top - topΔ,
                                           right = rect.right,
                                           bottom = rect.bottom - topΔ
                                       },
                                       topΔ == 0,
                                       TaskbarPosition.Top,
                                       monitor);
            }

            // taskbar on the left or right

            var leftΔ = rect.left - Screen.PrimaryScreen.Bounds.Left;

            if (leftΔ == 0)
                return new TaskbarInfo(handle,
                                       new TagRect
                                       {
                                           left = rect.left,
                                           top = rect.top,
                                           right = rect.right,
                                           bottom = rect.bottom
                                       },
                                       true,
                                       TaskbarPosition.Left,
                                       monitor);

            if (leftΔ < 0)
                return new TaskbarInfo(handle,
                                       new TagRect
                                       {
                                           left = rect.left - leftΔ,
                                           top = rect.top,
                                           right = rect.right - leftΔ,
                                           bottom = rect.bottom
                                       },
                                       false,
                                       TaskbarPosition.Left,
                                       monitor);

            var rightΔ = rect.right - Screen.PrimaryScreen.Bounds.Right;
            return new TaskbarInfo(handle,
                                   new TagRect
                                   {
                                       left = rect.left - rightΔ,
                                       top = rect.top,
                                       right = rect.right - rightΔ,
                                       bottom = rect.bottom
                                   },
                                   rightΔ == 0,
                                   TaskbarPosition.Right,
                                   monitor);
        }

        #endregion

        #region Show Or Hide Taskbar

        private const uint BarFlag = 0x05D1;

        private const uint TrayMonitorDefaulttoprimary = 1;
        private const uint TrayMonitorDefaulttonearest = 2;
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
                    taskbar.Monitor);
        }

        #endregion

        #region Determine whether it need to display the taskbar

        private const uint GaRoot = 2;
        private const int Tolerance = 20;

        private const string Progman = "Progman";
        private const string WorkerW = "WorkerW";
        private const string TaskListThumbnailWnd = "TaskListThumbnailWnd";
        private const string CoreWindow = "Windows.UI.Core.CoreWindow";

        /// <summary>
        ///     Mouse over the taskbar or a specific window,
        ///     it will only cause the taskbar to show or do nothing.
        /// </summary>
        /// <param name="taskbar"></param>
        /// <returns></returns>
        public static TaskbarBehavior CheckIfMouseOver(this in TaskbarInfo taskbar)
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
            return mouseOverHandle.GetName() is TaskListThumbnailWnd
                ? TaskbarBehavior.Show
                : TaskbarBehavior.Pending;
        }

        public static bool CheckIfWindowShouldHideTaskbar(this in TaskbarInfo taskbar, IntPtr foregroundHandle)
        {
            if (foregroundHandle == IntPtr.Zero)
                return false;
            // When the system is start up or a window is closed,
            // there is a certain probability that the taskbar will be set to foreground window.
            if (foregroundHandle == taskbar.Handle)
                return false;

            // Somehow, the foreground window is not necessarily visible.
            if (foregroundHandle.IsWindowInvisible())
                return false;

            var monitor = MonitorFromWindow(foregroundHandle, TrayMonitorDefaulttonearest);

            // If window is in another desktop, do not automatically hide the taskbar.
            if (monitor != taskbar.Monitor)
                return false;

            // Get foreground window Rectange.
            if (!GetWindowRect(foregroundHandle, out var rect))
                return false;

            // If the window and the taskbar do not intersect, the taskbar should be displayed.
            if (rect.bottom <= taskbar.Rect.top
                || rect.top >= taskbar.Rect.bottom
                || rect.left >= taskbar.Rect.right
                || rect.right <= taskbar.Rect.left)
                return false;

            // If the foreground Window is closing or idle, do nothing
            _ = GetWindowThreadProcessId(foregroundHandle, out var processId);
            if (processId == 0)
                return false;

            return true;
        }

        public static (TaskbarBehavior, ForegroundWindowInfo) CheckIfForegroundWindowIntersectTaskbar(
            this in TaskbarInfo taskbar)
        {
            var foregroundHandle = GetForegroundWindow();

            if (foregroundHandle == IntPtr.Zero)
                return (TaskbarBehavior.Pending, ForegroundWindowInfo.Empty);

            // When the system is start up or a window is closed,
            // there is a certain probability that the taskbar will be set to foreground window.
            if (foregroundHandle == taskbar.Handle)
                return (TaskbarBehavior.Show, ForegroundWindowInfo.Empty);

            // Somehow, the foreground window is not necessarily visible.
            if (foregroundHandle.IsWindowInvisible())
                return (TaskbarBehavior.Pending, ForegroundWindowInfo.Empty);

            var monitor = MonitorFromWindow(foregroundHandle, TrayMonitorDefaulttonearest);

            // If window is in another desktop, do not automatically hide the taskbar.
            if (monitor != taskbar.Monitor)
                return (TaskbarBehavior.Pending, ForegroundWindowInfo.Empty);

            // Get foreground window Rectange.
            if (!GetWindowRect(foregroundHandle, out var rect))
                return (TaskbarBehavior.Pending, ForegroundWindowInfo.Empty);

            // If the window and the taskbar do not intersect, the taskbar should be displayed.
            if (rect.bottom <= taskbar.Rect.top
                || rect.top >= taskbar.Rect.bottom
                || rect.left >= taskbar.Rect.right
                || rect.right <= taskbar.Rect.left)
                return (TaskbarBehavior.Show, new ForegroundWindowInfo(foregroundHandle, monitor, rect));

            // If the foreground Window is closing or idle, do nothing
            _ = GetWindowThreadProcessId(foregroundHandle, out var processId);
            if (processId == 0)
                return (TaskbarBehavior.DoNothing, new ForegroundWindowInfo(foregroundHandle, monitor, rect));

            switch (foregroundHandle.GetName())
            {
                // it's a desktop.
                case Progman:
                case WorkerW:
                    return (TaskbarBehavior.Show, new ForegroundWindowInfo(foregroundHandle, monitor, rect));
                // In rare circumstances, the start menu and search will not be displayed in the correct position,
                // causing the taskbar keep display, then hide, display, hide... in an endless loop.
                case CoreWindow:
                    return (TaskbarBehavior.DoNothing, new ForegroundWindowInfo(foregroundHandle, monitor, rect));
                default:
                    return (TaskbarBehavior.Hide, new ForegroundWindowInfo(foregroundHandle, monitor, rect));
            }
        }


        public static bool CheckIfDesktopShow(this in TaskbarInfo taskbar)
        {
            // Take a point on the taskbar to determine whether its current window is the desktop,
            // if it is, the taskbar should be displayed

            var window = GetWindowIntPtr(taskbar);

            if (window == IntPtr.Zero)
                return false;

            if (window == taskbar.Handle)
                return false;

            var rootWindow = GetAncestor(window, GaRoot);

            if (rootWindow == taskbar.Handle)
                return false;

            var name = rootWindow.GetName();

            switch (name)
            {
                case Progman:
                case WorkerW:
                    return true;
                default:
                    return false;
            }
        }

        private static IntPtr GetWindowIntPtr(in TaskbarInfo taskbar)
        {
            switch (taskbar.Position)
            {
                case TaskbarPosition.Bottom:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left, y = taskbar.Rect.top});
                case TaskbarPosition.Left:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.right, y = taskbar.Rect.top});
                case TaskbarPosition.Right:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left, y = taskbar.Rect.top});
                case TaskbarPosition.Top:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left, y = taskbar.Rect.bottom});
                default:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left, y = taskbar.Rect.top});
            }
        }

        #endregion
    }
}
