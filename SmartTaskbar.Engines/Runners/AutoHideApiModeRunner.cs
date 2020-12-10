using System;
using System.Collections.Generic;
using SmartTaskbar.Engines.Helpers;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;
using static SmartTaskbar.PlatformInvoke.SafeNativeMethods;

namespace SmartTaskbar.Engines.Runners
{
    internal class AutoHideApiModeRunner
        : IAutoModeMethod
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;
        private static int _counter;
        private readonly List<Taskbar> _taskbars;
        private readonly UserConfiguration _userConfiguration;

        public AutoHideApiModeRunner(UserConfiguration userConfiguration, List<Taskbar> taskbars)
        {
            _userConfiguration = userConfiguration;
            _taskbars = taskbars;

            Reset();
        }

        public AutoModeType Type { get; } = AutoModeType.AutoHideApiMode;


        public void Run()
        {
            if (_maxWindow != IntPtr.Zero)
            {
                _taskbars.MaintainBarState(_userConfiguration.TargetState);

                if (++_counter % Constants.MaxCount != 0) return;

                if (_maxWindow.IsWindowInvisible()
                    || _maxWindow.IsNotMaximizeWindow())
                    Ready();
                return;
            }

            _taskbars.MaintainBarState(_userConfiguration.ReadyState);

            if (++_counter % Constants.MaxCount != 0) return;

            if (_taskbars.IsMouseOverTaskbar()) return;

            _maxWindow = IntPtr.Zero;
            EnumWindows((handle, _) =>
                        {
                            if (handle.IsWindowInvisible()) return true;

                            if (handle.IsNotMaximizeWindow()) return true;

                            _maxWindow = handle;
                            return false;
                        },
                        IntPtr.Zero);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;

                _taskbars.SetBarState(_userConfiguration.ReadyState);
                return;
            }

            _taskbars.SetBarState(_userConfiguration.TargetState);
        }


        public void Reset()
            => Ready();


        public void Ready()
        {
            _maxWindow = IntPtr.Zero;
            _tryShowBar = true;
            _counter = 0;
        }
    }
}
