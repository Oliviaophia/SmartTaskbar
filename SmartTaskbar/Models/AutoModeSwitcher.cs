using System;
using System.Diagnostics;
using System.Timers;
using SmartTaskbar.Core;
using SmartTaskbar.Core.AutoMode;
using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Models
{
    public class AutoModeSwitcher : IDisposable
    {
        private static IAutoMode _autoMode;
        private static int _counter;
        private readonly CoreInvoker _coreInvoker;
        private readonly Timer _autoModeTimer = new Timer(125);

        public AutoModeSwitcher(CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;

            _autoModeTimer.Elapsed += AutoModeTimer_Elapsed;

            LoadSetting();
        }

        public void Dispose()
        {
            _autoModeTimer?.Dispose();
            ResetState();
        }

        private void AutoModeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _autoModeTimer.Stop();

            if (_counter % 97 == 0)
            {
                Ready();
                Debug.WriteLine("Ready");
            }

            if (_counter % 193 == 0)
            {
                Reset();
                _counter = 0;
                Debug.WriteLine("Reset");
            }

            Run();

            ++_counter;

            _autoModeTimer.Start();
        }

        public void LoadSetting() => SetAutoMode(_coreInvoker.UserSettings.ModeType);

        private void SetAutoMode(AutoModeType modeType)
        {
            _autoModeTimer.Stop();
            _autoMode = modeType switch
            {
                AutoModeType.Disable         => new DisableMode(_coreInvoker.UserSettings),
                AutoModeType.AutoHideApiMode => new AutoHideApiMode(_coreInvoker.UserSettings),
                AutoModeType.ForegroundMode  => new ForegroundMode(_coreInvoker.UserSettings),
                AutoModeType.BlockListMode    => new BlockListMode(_coreInvoker.UserSettings),
                AutoModeType.AllowlistMode   => new AllowlistMode(_coreInvoker.UserSettings),
                _                            => throw new ArgumentOutOfRangeException(nameof(modeType), modeType, null)
            };

            if (_autoMode != null)
                _autoModeTimer.Start();
            else
                ResetState();
        }

        private void ResetState() => InvokeMethods.ResetAutoModeState(_coreInvoker.UserSettings);

        private static void Run() => _autoMode?.Run();

        private static void Ready() => _autoMode?.Ready();

        private static void Reset() => _autoMode?.Reset();
    }
}