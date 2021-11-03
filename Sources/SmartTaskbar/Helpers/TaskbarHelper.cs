using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class TaskbarHelper
{
    public const string MainTaskbar = "Shell_TrayWnd";

    private const uint BarFlag = 0x05D1;

    internal static TaskbarInfo InitTaskbar()
    {
        var handle = FindWindow(MainTaskbar, null);

        _ = GetWindowRect(handle, out var rect);

        var heightΔ = rect.bottom - Screen.PrimaryScreen.Bounds.Bottom;

        return new TaskbarInfo(handle,
                               new TagRect
                               {
                                   left = rect.left, top = rect.top - heightΔ, right = rect.right,
                                   bottom = rect.bottom - heightΔ
                               });
    }

    internal static void HideTaskbar(this in TaskbarInfo taskbar)
    {
        _ = GetWindowRect(taskbar.Handle, out var rect);

        if (rect.bottom == taskbar.Rect.bottom)
            PostMessage(taskbar.Handle,
                        BarFlag,
                        IntPtr.Zero,
                        IntPtr.Zero);
    }

    internal static void ShowTaskar(this in TaskbarInfo taskbar, IntPtr monitorHandle)
    {
        _ = GetWindowRect(taskbar.Handle, out var rect);

        if (rect.bottom != taskbar.Rect.bottom)
            PostMessage(
                taskbar.Handle,
                BarFlag,
                (IntPtr) 1,
                monitorHandle);
    }

}
