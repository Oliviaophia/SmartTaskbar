using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class MouseHover
{
    private static IntPtr _lastHandle;
    private static IntPtr _currentHandle;
    private static bool _lastResult;

    internal static bool IsMouseOverTaskbar(this TaskbarInfo taskbar)
    {
        _ = GetCursorPos(out var point);
        _currentHandle = WindowFromPoint(point);
        if (_lastHandle == _currentHandle) return _lastResult;

        if (!taskbar.MonitorRectangle.Contains(point)) return _lastResult = false;

        _lastHandle = _currentHandle;
        var desktopHandle = GetDesktopWindow();
        while (_currentHandle != desktopHandle)
        {
            if (taskbar.TaskbarHandle == _currentHandle) return _lastResult = true;

            _currentHandle = _currentHandle.GetParentWindow();
        }

        return _lastResult = false;
    }
}
