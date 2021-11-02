using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class Worker
{
    private static HashSet<IntPtr> _cachedIntPtr;

    static Worker() { Reset(); }

    public static void Run()
    {
        if (TaskbarHelper.IsMouseOverTaskbar()) return;

        var foregroundHandle = GetForegroundWindow();

        if (foregroundHandle.IsWindowInvisible()) return;

        if (_cachedIntPtr.Contains(foregroundHandle))
        {
            TaskbarHelper.ShowTaskar();
            return;
        }

        switch (foregroundHandle.GetName())
        {
            case "Progman":
            case "WorkerW":
                _cachedIntPtr.Add(foregroundHandle);
                TaskbarHelper.ShowTaskar();
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
        _cachedIntPtr = new HashSet<IntPtr> {TaskbarHelper.Taskbar.TaskbarHandle};
        Ready();
    }

    public static void Ready()
    {
        if (AutoHideHelper.NotAutoHide())
            AutoHideHelper.SetAutoHide();
    }
}
