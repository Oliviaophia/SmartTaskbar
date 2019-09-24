using System;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using Timer = System.Timers.Timer;

namespace SmartTaskbar
{
    public class AutoModeController : IDisposable
    {
        private readonly Timer _timer = new Timer(375);

        public AutoModeController()
        {
            _timer.Elapsed += (sender, args) =>
            {
                InvokeMethods.AutoModeRun();
            };
            _timer.Start();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void AutoModeSet(AutoModeType autoModeType)
        {
            _timer.Stop();
            InvokeMethods.AutoModeSet(autoModeType);
            _timer.Start();
        }
    }
}
