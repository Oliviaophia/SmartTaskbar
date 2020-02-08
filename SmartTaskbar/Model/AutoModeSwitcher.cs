using System;
using System.Timers;
using SmartTaskbar.Core.AutoMode;
using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Model
{
    public class AutoModeSwitcher : IDisposable
    {
        private static IAutoMode _autoMode;
        private static int _counter;
        private readonly CoreInvoker _coreInvoker;
        private readonly Timer _timer = new Timer(375);

        public AutoModeSwitcher(CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;

            _timer.Elapsed += AutoModeTimer_Elapsed;

            LoadSetting();
        }

        public void Dispose() => _timer?.Dispose();

        private void AutoModeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_counter % 32 != 0)
            {
                Run();
            }
            else
            {
                if (_counter % 64 != 0)
                {
                    Ready();
                    Run();
                }
                else
                {
                    Reset();
                    Run();
                    _counter = 1;
                }
            }

            ++_counter;
        }

        public void LoadSetting() => SetAutoMode(_coreInvoker.UserSettings.ModeType);

        public void SetAutoMode(AutoModeType modeType)
        {
            _timer.Stop();
            _autoMode = modeType switch
            {
                AutoModeType.Disable => (IAutoMode) null,
                AutoModeType.AutoHideApiMode => (IAutoMode)null,
                AutoModeType.ForegroundMode => new ForegroundMode(),
                AutoModeType.BlacklistMode => new AutoMode(_coreInvoker.UserSettings),
                AutoModeType.WhitelistMode => new AutoMode(_coreInvoker.UserSettings),
                _ => throw new ArgumentOutOfRangeException(nameof(modeType), modeType, null)
            };
            _timer.Start();
        }

        public void Run() => _autoMode?.Run();

        public void Ready() => _autoMode?.Ready();

        public void Reset() => _autoMode?.Reset();
    }
}