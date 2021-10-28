using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal class AutoModeWorker
{
    private static bool _sendMessage;

    public AutoModeWorker() { Reset(); }

    public void Run()
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
        if (TaskbarHelper.Taskbar.TaskbarRectangle.IntersectsWith(rect) != TaskbarHelper.Taskbar.Intersect)
        {
            TaskbarHelper.Taskbar.Intersect = !TaskbarHelper.Taskbar.Intersect;
            _sendMessage = true;
        }


        if (!_sendMessage) return;
        _sendMessage = false;

        if (TaskbarHelper.Taskbar.Intersect)
        {
            TaskbarHelper.Taskbar.HideTaskbar();
        }
        else
        {
            TaskbarHelper.Taskbar.ShowTaskar();
        }
    }


    public void Reset()
    {
        
        Ready();
    }

    public void Ready()
    {
        _sendMessage = true;
        AutoHideHelper.SetAutoHide();
    }
}
