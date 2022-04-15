using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartTaskbar
{
    using static Fun;

    public static class TaskbarHelper

    {
        #region Initialize the taskbar Info

        /// <summary>
        ///     Main Taskbar Class Name
        /// </summary>
        private const string TrayMainTaskbarClassName = "Shell_TrayWnd";

        public static TaskbarInfo InitTaskbar()
        {
            // Find the main taskbar handle
            var handle = FindWindow(TrayMainTaskbarClassName, null);

            // unable to get the handle of the taskbar.
            if (handle == IntPtr.Zero)
                return TaskbarInfo.Empty;

            // Get taskbar window rectangle
            if (!GetWindowRect(handle, out var rect))
                // unable to get the rectangle of the taskbar.
                return TaskbarInfo.Empty;

            // determine the taskbar position

            var monitor = MonitorFromPoint(PointZero, TrayMonitorDefaultToPrimary);

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

        private const uint TrayBarFlag = 0x05D1;

        private const uint TrayMonitorDefaultToPrimary = 1;
        private const uint TrayMonitorDefaultToNearest = 2;
        private static readonly TagPoint PointZero = new TagPoint {x = 0, y = 0};

        /// <summary>
        ///     Hide the taskbar, in auto-hide mode
        /// </summary>
        /// <param name="taskbar"></param>
        public static void HideTaskbar(this in TaskbarInfo taskbar)
        {
            if (taskbar.IsShow)
                // Send a message to hide the taskbar, if taskbar is display
                _ = PostMessage(taskbar.Handle,
                                TrayBarFlag,
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
                    TrayBarFlag,
                    (IntPtr) 1,
                    taskbar.Monitor);
        }

        #endregion

        #region Determine whether it need to display the taskbar

        private const uint TrayGaRoot = 2;
        private const int TrayTolerance = 20;

        private const string TrayProgman = "Progman";
        private const string TrayWorkerW = "WorkerW";
        private const string TrayTaskListThumbnailWnd = "TaskListThumbnailWnd";
        private const string TrayCoreWindow = "Windows.UI.Core.CoreWindow";

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
            if (taskbar.Handle == GetAncestor(mouseOverHandle, TrayGaRoot))
                return TaskbarBehavior.DoNothing;

            // Some third-party software will parasitic on the taskbar
            // in order to prevent hide the taskbar by misjudgment.
            // Skip the windows that satisfy top and bottom in the range.
            if (GetWindowRect(mouseOverHandle, out var mouseOverRect)
                && mouseOverRect.top >= taskbar.Rect.top - TrayTolerance
                && mouseOverRect.bottom <= taskbar.Rect.bottom + TrayTolerance
                && mouseOverRect.left >= taskbar.Rect.left - TrayTolerance
                && mouseOverRect.right <= taskbar.Rect.right + TrayTolerance)
                return TaskbarBehavior.DoNothing;

            // If it is a thumbnail of the floating taskbar icon,
            // the taskbar needs to be displayed.
            return mouseOverHandle.GetClassName() is TrayTaskListThumbnailWnd
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

            var monitor = MonitorFromWindow(foregroundHandle, TrayMonitorDefaultToNearest);

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

            var monitor = MonitorFromWindow(foregroundHandle, TrayMonitorDefaultToNearest);

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

            switch (foregroundHandle.GetClassName())
            {
                // it's a desktop.
                case TrayProgman:
                case TrayWorkerW:
                    return (TaskbarBehavior.Show, new ForegroundWindowInfo(foregroundHandle, monitor, rect));
                // In rare circumstances, the start menu and search will not be displayed in the correct position,
                // causing the taskbar keep display, then hide, display, hide... in an endless loop.
                case TrayCoreWindow:
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

            var rootWindow = GetAncestor(window, TrayGaRoot);

            if (rootWindow == taskbar.Handle)
                return false;

            // Some third-party taskbar plugins will be attached to the taskbar location, but not embedded in the taskbar or desktop.

            // Get foreground window Rectange.
            if (!GetWindowRect(rootWindow, out var rect))
                return true;

            if (3 * rect.Area < Screen.PrimaryScreen.Bounds.Width * Screen.PrimaryScreen.Bounds.Height)
                return true;

            switch (rootWindow.GetClassName())
            {
                case TrayProgman:
                case TrayWorkerW:
                    #if DEBUG
                    Debug.WriteLine("Show the tasbkar because of Desktop Show");
                    #endif
                    return true;
                default:
                    return false;
            }
        }

        private static IntPtr GetWindowIntPtr(in TaskbarInfo taskbar)
        {
            // The maximized application on the next desktop will be extended to the current desktop.
            // Therefore a certain tolerance is necessary.
            switch (taskbar.Position)
            {
                case TaskbarPosition.Bottom:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left + TrayTolerance, y = taskbar.Rect.top});
                case TaskbarPosition.Left:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.right, y = taskbar.Rect.top + TrayTolerance});
                case TaskbarPosition.Right:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left, y = taskbar.Rect.top + TrayTolerance});
                case TaskbarPosition.Top:
                    return WindowFromPoint(
                        new TagPoint {x = taskbar.Rect.left + TrayTolerance, y = taskbar.Rect.bottom});
                default:
                    return WindowFromPoint(new TagPoint {x = taskbar.Rect.left + TrayTolerance, y = taskbar.Rect.top});
            }
        }

        #endregion
    }
}
