using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class Worker
{
    private static bool _sendMessage;
    private static bool _intersect;

    static Worker() { Reset(); }

    public static void Run()
    {
        if (TaskbarHelper.Taskbar.IsMouseOverTaskbar())
        {
            _sendMessage = true;
            return;
        }

        var foregroundHandle = GetForegroundWindow();
        if (foregroundHandle.IsWindowInvisible()) return;

        switch (foregroundHandle.GetName())
        {
            case "Windows.UI.Core.CoreWindow":
                _sendMessage = true;
                return;
            case "XamlExplorerHostIslandWindow":
            case "WorkerW":
            case "Progman":
            case "DV2ControlHost":
            case Constants.MainTaskbar:
            case Constants.SubTaskbar:
            case "MultitaskingViewFrame":
            case "WindowsDashboard":
            case "VirtualDesktopGestureSwitcher":
            case "ForegroundStaging":
            case "NotifyIconOverflowWindow":
            case "TrayiconMessageWindow":
                return;
        }


        _ = GetWindowRect(foregroundHandle, out var rect);
        if (TaskbarHelper.Taskbar.TaskbarRectangle.IntersectsWith(rect) != _intersect)
        {
            _intersect = !_intersect;
            _sendMessage = true;
        }


        if (!_sendMessage) return;
        _sendMessage = false;

        if (_intersect)
        {
            TaskbarHelper.Taskbar.HideTaskbar();
        }
        else
        {
            TaskbarHelper.Taskbar.ShowTaskar();
        }
    }


    public static void Reset()
    {
        TaskbarHelper.UpdateTaskbarInfo();
        Ready();
    }

    public static void Ready()
    {
        _intersect = false;
        _sendMessage = true;
        if (AutoHideHelper.NotAutoHide())
            AutoHideHelper.SetAutoHide();
    }
}
