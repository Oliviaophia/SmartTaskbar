using System.Diagnostics;
using static SmartTaskbar.SafeNativeMethods;
using Timer = System.Threading.Timer;

namespace SmartTaskbar;

internal class Engine : IDisposable
{
    private static readonly Timer Timer = new(TimerCallback);
    
    public void Dispose()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        Timer.Dispose();
    }

    private static void TimerCallback(object? state)
    {
        // Make sure the taskbar has been automatically hidden, otherwise it will not work
        AutoHideHelper.SetAutoHide();
        var taskbar =TaskbarHelper.InitTaskbar();

        #region Run

        var foregroundHandle = GetForegroundWindow();

        if (foregroundHandle.IsWindowInvisible()) return;

        if (taskbar.IsMouseOverWhitelist()) return;

        var name = foregroundHandle.GetName();
        switch (name)
        {
            // Determine whether it is a desktop.
            case "WorkerW":
            case "Progman":
                taskbar.ShowTaskar();
                return;
        }
        //Debug.WriteLine(name);

        // Get foreground window Rectange
        _ = GetWindowRect(foregroundHandle, out var rect);
        if (rect.bottom > taskbar.Rect.top &&
            rect.top < taskbar.Rect.bottom &&
            rect.left < taskbar.Rect.right &&
            rect.right > taskbar.Rect.left)
            taskbar.HideTaskbar();
        else
            taskbar.ShowTaskar();

        #endregion
    }

    /// <summary>
    ///     Turn off the timer, Pause auto mode
    /// </summary>
    public static void Stop()
        => Timer.Change(Timeout.Infinite, Timeout.Infinite);

    /// <summary>
    ///     Start the timer, start the auto mode
    /// </summary>
    public static void Start()
    {
        // 125 milliseconds is a balance between user-acceptable perception and system call time
        Timer.Change(125, 125);
    }
}
