using System;
using System.Collections.Generic;
using SmartTaskbar.Engines.Helpers;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Engines.Runners;
using SmartTaskbar.Models;

namespace SmartTaskbar.Engines
{
    public class AutoModeWorker : IAutoModeMethod
    {
        private static IAutoModeMethod _autoModeRunner;

        private static List<Taskbar> _taskbars = new(4);

        private readonly IUserConfigEngine _userConfigEngine;

        public AutoModeWorker(IUserConfigEngine userConfigEngine)
        {
            _userConfigEngine = userConfigEngine;
            UpdateTaskbarList();
        }

        public AutoModeType Type
            => _userConfigEngine.UserConfiguration.AutoModeType;

        public void Run() { _autoModeRunner.Run(); }

        public void Reset()
        {
            _autoModeRunner.Reset();
            UpdateTaskbarList();
        }

        public void Ready()
        {
            _autoModeRunner.Ready();
            UpdateTaskbarList();
        }

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
                AutoModeType.AllowListMode => _autoModeRunner.Type == AutoModeType.AllowListMode
                    ? _autoModeRunner
                    : _autoModeRunner = new AllowlistModeRunner(),
                _ => throw new ArgumentOutOfRangeException()
            };

        public static void UpdateTaskbarList() { _taskbars.ResetTaskbars(); }
    }
}
