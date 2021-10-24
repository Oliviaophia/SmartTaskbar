using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal class AutoModeWorker
{
    private static readonly List<Taskbar> Taskbars = new();
    private static bool _sendMessage;

    public AutoModeWorker() { Reset(); }

    public void Run()
    {
        if (Taskbars.IsMouseOverTaskbar())
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

        if (foregroundHandle.IsNotMaximizeWindow()
            || foregroundHandle.IsNotFullScreenWindow())
        {
            _ = GetWindowRect(foregroundHandle, out var rect);
            foreach (var taskbar in Taskbars.Where(
                         taskbar => (rect.left < taskbar.Rectangle.Right
                                     && rect.right > taskbar.Rectangle.Left
                                     && rect.top < taskbar.Rectangle.Bottom
                                     && rect.bottom > taskbar.Rectangle.Top)
                                    != taskbar.Intersect))
            {
                taskbar.Intersect = !taskbar.Intersect;
                _sendMessage = true;
            }
        }
        else
        {
            var monitor = foregroundHandle.GetMonitor();
            foreach (var taskbar in Taskbars.Where(taskbar =>
                                                       taskbar.Monitor == monitor != taskbar.Intersect))
            {
                taskbar.Intersect = !taskbar.Intersect;
                _sendMessage = true;
            }
        }

        if (!_sendMessage) return;
        _sendMessage = false;

        foreach (var taskbar in Taskbars.Where(taskbar => !taskbar.Intersect))
        {
            taskbar.Monitor.ShowTaskar();
            return;
        }

        PostMessageHelper.HideTaskbar();
    }


    public void Reset()
    {
        _ = Taskbars.ResetTaskbars();
        Ready();
    }

    public void Ready()
    {
        _sendMessage = true;
        AutoHideHelper.SetAutoHide();
    }
}
