using System.Timers;
using Timer = System.Timers.Timer;

namespace SmartTaskbar
{
    internal class Engine: IDisposable
    {
        private static readonly Timer timer = new Timer(125);
        private static int _counter = 0;
        private static readonly AutoModeWorker _worker = new AutoModeWorker();

        public Engine()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            timer.Stop();

            if (_counter % 97 == 0) _worker.Ready();

            if (_counter % 193 == 0)
            {
                _worker.Reset();
                _counter = 0;
            }

            _worker.Run();

            ++_counter;

            timer.Start();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
