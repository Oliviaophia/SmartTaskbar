using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class TaskbarHelper
{

    private const uint MonitorDefaultToPrimary = 1;

    public const string MainTaskbar = "Shell_TrayWnd";

    private const uint BarFlag = 0x05D1;
    private static readonly TagPoint PointZero = new() {x = 0, y = 0};

    internal static TaskbarInfo InitTaskbar()
    {
        var taskbarHandle = FindWindow(MainTaskbar, null);

        _ = GetWindowRect(taskbarHandle, out var rect);

        var heightΔ = rect.bottom - Screen.PrimaryScreen.Bounds.Bottom;

        return new TaskbarInfo(taskbarHandle,
                               MonitorFromPoint(PointZero, MonitorDefaultToPrimary),
                               new TagRect
                               {
                                   left = rect.left, top = rect.top - heightΔ, right = rect.right,
                                   bottom = rect.bottom - heightΔ
                               },
                               Screen.PrimaryScreen.Bounds);
    }

    internal static void HideTaskbar(this in TaskbarInfo taskbar)
    {
        _ = GetWindowRect(taskbar.TaskbarHandle, out var rect);

        if (rect.bottom == taskbar.MonitorRectangle.Bottom)
            PostMessage(taskbar.TaskbarHandle,
                        BarFlag,
                        IntPtr.Zero,
                        IntPtr.Zero);
    }

    internal static void ShowTaskar(this in TaskbarInfo taskbar)
    {
        _ = GetWindowRect(taskbar.TaskbarHandle, out var rect);

        if (rect.bottom != taskbar.MonitorRectangle.Bottom)
            PostMessage(
                taskbar.TaskbarHandle,
                BarFlag,
                (IntPtr) 1,
                taskbar.MonitorHandle);
    }

}
