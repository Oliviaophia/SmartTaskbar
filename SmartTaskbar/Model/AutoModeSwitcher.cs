using System;
using System.Timers;
using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Model
{
    public class AutoModeSwitcher : IDisposable
    {
        private readonly CoreInvoker _coreInvoker;
        private readonly Timer _timer = new Timer(375);

        public AutoModeSwitcher(CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;

            // todo 
        }

        public void SetAutoMode(AutoModeType modeType)
        {
            // todo 
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}