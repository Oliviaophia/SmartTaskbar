using System;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;

namespace SmartTaskbar.Engines.Runners
{
    internal class AutoHideApiModeRunner
        : IAutoModeMethod
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;
        private static int _counter;
        private readonly UserConfiguration _userConfiguration;

        public AutoHideApiModeRunner(UserConfiguration userConfiguration)
        {
            _userConfiguration = userConfiguration;

            Reset();
        }

        public AutoModeType Type { get; } = AutoModeType.AutoHideApiMode;

        public void Run()
        {
            if (_maxWindow != IntPtr.Zero) { }
        }

        public void Reset() { throw new NotImplementedException(); }

        public void Ready() { throw new NotImplementedException(); }
    }
}
