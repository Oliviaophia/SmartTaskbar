using System.Timers;

namespace SmartTaskbar;

internal class Engine : IDisposable
{
    private static readonly global::System.Timers.Timer timer = new(125);
    private static int _counter;
    private static readonly AutoModeWorker _worker = new();
    private static bool RunningFlag = true;

    public Engine()
    {
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    public void Dispose()
    {
        RunningFlag = false;
        timer?.Stop();
        timer?.Dispose();
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        timer?.Stop();

        if (!RunningFlag)
            return;

        if (_counter % 97 == 0) _worker.Ready();

        if (_counter % 193 == 0)
        {
            _worker.Reset();
            _counter = 0;
        }

        _worker.Run();

        ++_counter;

        if (RunningFlag)
            timer?.Start();
    }

    public void Stop()
    {
        timer?.Stop();
        RunningFlag = false;
    }

    public void Start()
    {
        timer?.Start();
        RunningFlag = true;
    }
}
