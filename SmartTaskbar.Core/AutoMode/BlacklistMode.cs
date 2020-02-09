using System;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class BlacklistMode : IAutoMode
    {
        private readonly UserSettings _userSettings;
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;

        public BlacklistMode(UserSettings userSettings)
        {
            _userSettings = userSettings;
            Reset();
        }

        public void Run()
        {
            if (_maxWindow != IntPtr.Zero)
            {
                if (_maxWindow.IsWindowInvisible() || _maxWindow.IsNotMaximizeWindow()) Ready();
                return;
            }

            if (Variable.Taskbars.IsMouseOverTaskbar()) return;

            _maxWindow = IntPtr.Zero;
            EnumWindows((handle, lp) =>
            {
                if (handle.IsWindowInvisible()) return true;

                if (handle.IsNotMaximizeWindow()) return true;

                if (handle.InBlacklist(_userSettings)) return true;

                _maxWindow = handle;
                return false;
            }, IntPtr.Zero);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;

                ButtonSize.SetIconSize(_userSettings.ReadyState.IconSize);
                AutoHide.SetAutoHide(_userSettings.ReadyState.IsAutoHide);
                return;
            }

            ButtonSize.SetIconSize(_userSettings.TargetState.IconSize);
            AutoHide.SetAutoHide(_userSettings.TargetState.IsAutoHide);
        }

        public void Ready()
        {
            _maxWindow = IntPtr.Zero;
            _tryShowBar = true;
        }

        public void Reset()
        {
            Ready();
            Variable.Taskbars.ResetTaskbars();
            Variable.NameCache.UpdateCacheName();
        }
    }
}