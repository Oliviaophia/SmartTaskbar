using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class Worker
{
    private static IntPtr _desktopHandle;

    static Worker() { Reset(); }

    public static void Run()
    {
        if (TaskbarHelper.IsMouseOverTaskbar(_desktopHandle))
        {
            return;
        }

        var foregroundHandle = GetForegroundWindow();

        if (foregroundHandle.IsWindowInvisible()) return;

        var name = foregroundHandle.GetName();
        switch (name)
        {
            case "Windows.UI.Core.CoreWindow":
                return;
            case "XamlExplorerHostIslandWindow":
            case "WorkerW":
                TaskbarHelper.ShowTaskar();
                return;
            case "Progman":
            case "DV2ControlHost": 
            case TaskbarHelper.MainTaskbar:
            case "MultitaskingViewFrame":
            case "WindowsDashboard":
            case "VirtualDesktopGestureSwitcher":
            case "ForegroundStaging":
            case "NotifyIconOverflowWindow":
            case "TrayiconMessageWindow":
                return;
        }


        _ = GetWindowRect(foregroundHandle, out var rect);
        if (TaskbarHelper.Taskbar.TaskbarRectangle.IntersectsWith(rect))
            TaskbarHelper.HideTaskbar();
        else
            TaskbarHelper.ShowTaskar();
    }


    public static void Reset()
    {
        TaskbarHelper.UpdateTaskbarInfo();
        Ready();
    }

    public static void Ready()
    {
        _desktopHandle = GetDesktopWindow();
        if (AutoHideHelper.NotAutoHide())
            AutoHideHelper.SetAutoHide();
    }
}
