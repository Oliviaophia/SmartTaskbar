using System.Timers;

using Timer = System.Timers.Timer;

namespace SmartTaskbar;

internal class Engine : IDisposable
{
    private static readonly Timer Timer = new(125);
    private static int _counter;
    private static bool _runningFlag = true;

    public Engine()
    {
        Timer.Elapsed += Timer_Elapsed;
    }
    
    public void Dispose()
    {
        _runningFlag = false;
        Timer?.Stop();
        Timer?.Dispose();
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Timer?.Stop();

        if (!_runningFlag)
            return;

        if (_counter % 97 == 0) Worker.Ready();

        if (_counter % 193 == 0)
        {
            Worker.Reset();
            _counter = 0;
        }

        Worker.Run();

        ++_counter;

        if (_runningFlag)
            Timer?.Start();
    }

    public void Stop()
    {
        Timer?.Stop();
        _runningFlag = false;
    }

    public void Start()
    {
        Timer?.Start();
        _runningFlag = true;
    }
}
