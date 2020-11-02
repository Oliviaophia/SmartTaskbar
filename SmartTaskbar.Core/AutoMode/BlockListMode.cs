using System;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class BlockListMode : IAutoMode
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;
        private static int _counter;
        private readonly UserSettings _userSettings;

        public BlockListMode(UserSettings userSettings)
        {
            _userSettings = userSettings;
            Reset();
        }

        public void Run()
        {
            if (_maxWindow != IntPtr.Zero)
            {
                Variable.Taskbars.MaintainBarState(_userSettings.TargetState);

                if (++_counter % Constant.MaxCount != 0) return;

                if (_maxWindow.IsWindowInvisible()
                    || _maxWindow.IsNotMaximizeWindow()) Ready();
                return;
            }

            Variable.Taskbars.MaintainBarState(_userSettings.ReadyState);

            if (++_counter % Constant.MaxCount != 0) return;

            if (Variable.Taskbars.IsMouseOverTaskbar()) return;

            _maxWindow = IntPtr.Zero;
            EnumWindows((handle, lp) =>
                        {
                            if (handle.IsWindowInvisible()) return true;

                            if (handle.IsNotMaximizeWindow()) return true;

                            if (handle.InBlockList(_userSettings.BlockList)) return true;

                            _maxWindow = handle;
                            return false;
                        },
                        IntPtr.Zero);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;

                Variable.Taskbars.SetBarState(_userSettings.ReadyState);
                return;
            }

            Variable.Taskbars.SetBarState(_userSettings.TargetState);
        }

        public void Ready()
        {
            _maxWindow = IntPtr.Zero;
            _tryShowBar = true;
            _counter = 0;
        }

        public void Reset()
        {
            Ready();
            Variable.Taskbars.ResetTaskbars();
            Variable.NameCache.UpdateCacheName();
        }
    }
}
