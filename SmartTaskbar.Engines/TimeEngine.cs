using System;
using System.Timers;

namespace SmartTaskbar.Engines
{
    public class TimeEngine : IDisposable
    {
        private readonly AutoModeWorker _autoModeWorker;
        private readonly Timer _mainTimer;


        private int _counter;


        public TimeEngine(AutoModeWorker autoModeWorker)
        {
            _autoModeWorker = autoModeWorker;

            _mainTimer = new Timer(125);

            _mainTimer.Elapsed += MainTimer_Elapsed;
            _mainTimer.Start();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
            => _mainTimer.Dispose();

        private void MainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _mainTimer.Stop();

            if (_counter % 97 == 0) _autoModeWorker.Ready();

            if (_counter % 193 == 0)
            {
                _autoModeWorker.Reset();
                _counter = 0;
            }

            _autoModeWorker.Run();

            ++_counter;

            _mainTimer.Start();
        }
    }
}
