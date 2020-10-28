using System;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Engines.ModeMethods;
using SmartTaskbar.Models;

namespace SmartTaskbar.Engines
{
    public class AutoModeWorker
    {
        private static IAutoModeMethod _autoModeMethod;
        private readonly UserConfigEngine _userConfigEngine;

        public AutoModeWorker(UserConfigEngine userConfigEngine)
            => _userConfigEngine = userConfigEngine;

        public void Run() { }

        public IAutoModeMethod AutoModelSelector()
            => _userConfigEngine.UserConfiguration.AutoModeType switch
            {
                AutoModeType.Disable => _autoModeMethod = null,
                AutoModeType.AutoHideApiMode => _autoModeMethod.Type == AutoModeType.AutoHideApiMode
                    ? _autoModeMethod
                    : _autoModeMethod = new AutoHideApiModeMethod(),
                AutoModeType.ForegroundMode => _autoModeMethod.Type == AutoModeType.ForegroundMode
                    ? _autoModeMethod
                    : _autoModeMethod = new ForegroundModeMethod(),
                AutoModeType.BlockListMode => _autoModeMethod.Type == AutoModeType.BlockListMode
                    ? _autoModeMethod
                    : _autoModeMethod = new BlockListModeMethod(),
                AutoModeType.AllowlistMode => _autoModeMethod.Type == AutoModeType.AllowlistMode
                    ? _autoModeMethod
                    : _autoModeMethod = new AllowlistModeMethod(),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
