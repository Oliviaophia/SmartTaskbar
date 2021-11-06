using System.Timers;
using Timer = System.Timers.Timer;

namespace SmartTaskbar;

public class Engine : IDisposable
{
    private static Timer? _timer;

    private static bool _enabled;

    public Engine()
    {
        // 125 milliseconds is a balance between user-acceptable perception and system call time.
        _timer = new Timer(125);
        _timer.Elapsed += Timer_Elapsed;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        _timer?.Stop();
        // Make sure the taskbar has been automatically hidden, otherwise it will not work
        Fun.SetAutoHide();
        var taskbar = TaskbarHelper.InitTaskbar();

        var behavior = taskbar.ShouldMouseOverWindowShowTheTaskbar();

        if (behavior == TaskbarBehavior.Pending)
        {
            behavior = taskbar.ShouldForegroundWindowShowTheTaskbar();
            if (behavior == TaskbarBehavior.Pending)
                behavior = taskbar.ShouldDesktopShowTheTaskbar();
        }

        switch (behavior)
        {
            case TaskbarBehavior.Show:
                taskbar.ShowTaskar();
                break;
            case TaskbarBehavior.Hide:
                taskbar.HideTaskbar();
                break;
        }

        if (_enabled)
            _timer?.Start();
    }

    /// <summary>
    ///     Turn off the timer, Pause auto mode
    /// </summary>
    public static void Stop()
    {
        _enabled = false;
        _timer?.Stop();
    }

    /// <summary>
    ///     Start the timer, start the auto mode
    /// </summary>
    public static void Start()
    {
        _enabled = true;
        _timer?.Start();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        if (_timer is null) return;

        _timer?.Dispose();
        _timer = null;
    }
}
