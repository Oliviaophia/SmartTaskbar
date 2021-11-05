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

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        _timer?.Stop();
        // Make sure the taskbar has been automatically hidden, otherwise it will not work
        AutoHideHelper.SetAutoHide();
        var taskbar = TaskbarHelper.InitTaskbar();

        switch (taskbar.ShouldMouseOverWindowShowTheTaskbar())
        {
            case TaskbarBehavior.Pending:
                switch (taskbar.ShouldForegroundWindowShowTheTaskbar())
                {
                    case TaskbarBehavior.Show:
                        taskbar.ShowTaskar();
                        break;
                    case TaskbarBehavior.Hide:
                        taskbar.HideTaskbar();
                        break;
                    case TaskbarBehavior.Pending:
                        switch (taskbar.ShouldVisibleWindowShowTheTaskbar())
                        {
                            case TaskbarBehavior.Show:
                                taskbar.ShowTaskar();
                                break;
                            case TaskbarBehavior.Hide:
                                taskbar.HideTaskbar();
                                break;
                            case TaskbarBehavior.Pending:
                                break;
                            case TaskbarBehavior.DoNothing:
                                break;
                        }
                        break;
                    case TaskbarBehavior.DoNothing:
                        break;
                }
                break;
            case TaskbarBehavior.Show:
                taskbar.ShowTaskar();
                break;
            case TaskbarBehavior.DoNothing:
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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_timer is not null)
            {
                _timer?.Dispose();
                _timer = null;
            }
        }
    }
}