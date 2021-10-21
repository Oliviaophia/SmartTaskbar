using System.Timers;
using Timer = System.Timers.Timer;

namespace SmartTaskbar
{
    internal class Engine: IDisposable
    {
        private static readonly Timer timer = new Timer(125);
        private static int _counter = 0;
        private static readonly AutoModeWorker _worker = new AutoModeWorker();
        private static bool RunningFlag = true;

        public Engine()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
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

        public void Dispose()
        {
            RunningFlag = false;
            timer?.Stop();
            timer?.Dispose();
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
}
