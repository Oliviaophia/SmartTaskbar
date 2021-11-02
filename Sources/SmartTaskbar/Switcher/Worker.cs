using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class Worker
{
    static Worker() { Reset(); }

    public static void Run()
    {
        if (TaskbarHelper.IsMouseOverTaskbar())
        {
            return;
        }

        var foregroundHandle = GetForegroundWindow();

        if (foregroundHandle.IsWindowInvisible()) return;

        if (foregroundHandle == TaskbarHelper.Taskbar.TaskbarHandle)
        {
            TaskbarHelper.ShowTaskar();
            return;
        }
        
        switch (foregroundHandle.GetName())
        {
            case "Progman":
            case "WorkerW":
            case TaskbarHelper.MainTaskbar:
                TaskbarHelper.ShowTaskar();
                return;
            //case "Windows.UI.Core.CoreWindow":
            //case "XamlExplorerHostIslandWindow":
            //case "DV2ControlHost":           
            //case "MultitaskingViewFrame":
            //case "WindowsDashboard":
            //case "VirtualDesktopGestureSwitcher":
            //case "ForegroundStaging":
            //case "NotifyIconOverflowWindow":
            //case "TrayiconMessageWindow":
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
        if (AutoHideHelper.NotAutoHide())
            AutoHideHelper.SetAutoHide();
    }
}
