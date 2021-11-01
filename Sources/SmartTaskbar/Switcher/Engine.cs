using Timer = System.Threading.Timer;

namespace SmartTaskbar;

internal class Engine : IDisposable
{
    private static readonly Timer Timer = new(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    private static int _counter;
    private static bool _runningFlag = true;

    public void Dispose()
    {
        _runningFlag = false;
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        Timer.Dispose();
    }

    private static void TimerCallback(object? state)
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);

        if (!_runningFlag)
            return;

        if (_counter == 80) Worker.Ready();

        if (_counter == 320)
        {
            Worker.Reset();
            _counter = 0;
        }

        Worker.Run();

        ++_counter;

        if (_runningFlag)
            Timer.Change(125, Timeout.Infinite);
    }

    public static void Stop()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        _runningFlag = false;
    }

    public static void Start()
    {
        Timer.Change(125, Timeout.Infinite);
        _runningFlag = true;
    }
}
