using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class TaskbarHelper
{
    private static IntPtr _lastHandle;
    private static IntPtr _currentHandle;
    private static bool _lastResult;

    private const uint GaParent = 1;

    private const uint MONITOR_DEFAULTTOPRIMARY = 1;
    private static readonly TagPoint PointZero = new() { x = 0, y = 0 };

    public const string MainTaskbar = "Shell_TrayWnd";

    private static TaskbarInfo? _taskbar;

    internal static TaskbarInfo Taskbar => _taskbar ??= InitTaskbar();

    internal static void UpdateTaskbarInfo()
        => _taskbar = InitTaskbar();

    private static TaskbarInfo InitTaskbar()
    {
        var taskbarHandle = FindWindow(MainTaskbar, null);

        _ = GetWindowRect(taskbarHandle, out var rect);

        var heightΔ = rect.bottom - Screen.PrimaryScreen.Bounds.Bottom;

        return new TaskbarInfo(taskbarHandle, MonitorFromPoint(PointZero, MONITOR_DEFAULTTOPRIMARY), new TagRect { left = rect.left, top = rect.top - heightΔ, right= rect.right, bottom= rect.bottom - heightΔ }, Screen.PrimaryScreen.Bounds);
    }

    private const uint BarFlag = 0x05D1;

    internal static void HideTaskbar()
    {
        _ = GetWindowRect(Taskbar.TaskbarHandle, out var rect);

        if (rect.bottom == Taskbar.MonitorRectangle.bottom)
            PostMessage(Taskbar.TaskbarHandle,
                              BarFlag,
                              IntPtr.Zero,
                              IntPtr.Zero);
    }

    internal static void ShowTaskar()
    {
        _ = GetWindowRect(Taskbar.TaskbarHandle, out var rect);

        if (rect.bottom != Taskbar.MonitorRectangle.bottom)
            PostMessage(
                   Taskbar.TaskbarHandle,
                   BarFlag,
                   (IntPtr)1,
                   Taskbar.MonitorHandle);
    }

    internal static bool IsMouseOverTaskbar()
    {
        _ = GetCursorPos(out var point);
        _currentHandle = WindowFromPoint(point);
        if (_lastHandle == _currentHandle) return _lastResult;

        if (!Taskbar.MonitorRectangle.Contains(point)) return _lastResult = false;

        _lastHandle = _currentHandle;
        var desktopHandle = GetDesktopWindow();
        while (_currentHandle != desktopHandle)
        {
            if (Taskbar.TaskbarHandle == _currentHandle) return _lastResult = true;

            _currentHandle = GetAncestor(_currentHandle, GaParent);
        }

        return _lastResult = false;
    }

}
