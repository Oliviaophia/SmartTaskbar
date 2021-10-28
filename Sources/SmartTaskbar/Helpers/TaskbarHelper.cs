using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class TaskbarHelper
{
    private static TaskbarInfo? _taskbar;

    internal static TaskbarInfo Taskbar => _taskbar ??= InitTaskbar();

    internal static void UpdateTaskbarInfo()
        => _taskbar = InitTaskbar();

    private static TaskbarInfo InitTaskbar()
    {
        var taskbarHandle = FindWindow(Constants.MainTaskbar, null);

        _ = GetWindowRect(taskbarHandle, out var rect);

        var heightΔ = rect.bottom - Screen.PrimaryScreen.Bounds.Bottom;

        return new TaskbarInfo(taskbarHandle, MonitorHelper.GetPrimaryMonitor(), new TagRect { left = rect.left, top = rect.top - heightΔ, right= rect.right, bottom= rect.bottom - heightΔ }, Screen.PrimaryScreen.Bounds);
    }
}
