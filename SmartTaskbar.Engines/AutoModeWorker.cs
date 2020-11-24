using System;
using System.Collections.Generic;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Engines.Runners;
using SmartTaskbar.Models;

namespace SmartTaskbar.Engines
{
    public class AutoModeWorker
    {
        private static IAutoModeMethod _autoModeRunner;

        private static List<Taskbar> _taskbars = new List<Taskbar>(4);

        private readonly IUserConfigEngine _userConfigEngine;

        public AutoModeWorker(IUserConfigEngine userConfigEngine)
        {
            _userConfigEngine = userConfigEngine;
            UpdateTaskbarList();
        }

        public void Run() { }

        public IAutoModeMethod AutoModelSelector()
            => _userConfigEngine.UserConfiguration.AutoModeType switch
            {
                AutoModeType.Disable => _autoModeRunner = null,
                AutoModeType.AutoHideApiMode => _autoModeRunner.Type == AutoModeType.AutoHideApiMode
                    ? _autoModeRunner
                    : _autoModeRunner = new AutoHideApiModeRunner(_userConfigEngine.UserConfiguration),
                AutoModeType.ForegroundMode => _autoModeRunner.Type == AutoModeType.ForegroundMode
                    ? _autoModeRunner
                    : _autoModeRunner = new ForegroundModeRunner(),
                AutoModeType.BlockListMode => _autoModeRunner.Type == AutoModeType.BlockListMode
                    ? _autoModeRunner
                    : _autoModeRunner = new BlockListModeRunner(),
                AutoModeType.AllowlistMode => _autoModeRunner.Type == AutoModeType.AllowlistMode
                    ? _autoModeRunner
                    : _autoModeRunner = new AllowlistModeRunner(),
                _ => throw new ArgumentOutOfRangeException()
            };

        public void UpdateTaskbarList() { }
    }
}
