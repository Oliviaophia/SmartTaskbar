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
        private readonly List<Taskbar> _taskbars = new(4);

        private readonly IUserConfigEngine _userConfigEngine;
        private IAutoModeMethod? _autoModeRunner;

        public AutoModeWorker(IUserConfigEngine userConfigEngine)
        {
            _userConfigEngine = userConfigEngine;
            UpdateTaskbarList();
        }

        public AutoModeType Type { get; private set; }

        public void Run()
            => AutoModelSelector()?.Run();

        public void Reset()
            => AutoModelSelector()?.Reset();

        public void Ready()
            => AutoModelSelector()?.Ready();

        public IAutoModeMethod? AutoModelSelector()
        {
            Type = _userConfigEngine.UserConfiguration.AutoModeType;

            if (Type != AutoModeType.Disable
                || _autoModeRunner is null)
                return Type switch
                {
                    AutoModeType.Disable => _autoModeRunner,
                    AutoModeType.AutoHideApiMode => _autoModeRunner?.Type == AutoModeType.AutoHideApiMode
                        ? _autoModeRunner
                        : _autoModeRunner = new AutoHideApiModeRunner(_userConfigEngine.UserConfiguration, _taskbars),
                    AutoModeType.ForegroundMode => _autoModeRunner?.Type == AutoModeType.ForegroundMode
                        ? _autoModeRunner
                        : _autoModeRunner = new ForegroundModeRunner(),
                    AutoModeType.BlockListMode => _autoModeRunner?.Type == AutoModeType.BlockListMode
                        ? _autoModeRunner
                        : _autoModeRunner = new BlockListModeRunner(),
                    AutoModeType.AllowListMode => _autoModeRunner?.Type == AutoModeType.AllowListMode
                        ? _autoModeRunner
                        : _autoModeRunner = new AllowlistModeRunner(),
                    _ => throw new ArgumentOutOfRangeException()
                };

            ResetTaskbarState();

            return _autoModeRunner = null;
        }

        public void UpdateTaskbarList() { _taskbars.ResetTaskbars(); }

        public void ResetTaskbarState()
            => _taskbars.SetBarState(_userConfigEngine.UserConfiguration.ResetState);
    }
}
