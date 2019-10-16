using System;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class AutoMode : IAutoMode
    {
        private readonly UserSettings _userSettings;
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoMode(UserSettings userSettings)
        {
            _userSettings = userSettings;
            Ready();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ready()
        {
            _maxWindow = IntPtr.Zero;
            _tryShowBar = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            if (_maxWindow != IntPtr.Zero)
            {
                if (_maxWindow.IsWindowInvisible() || _maxWindow.IsNotMaximizeWindow()) Ready();
                return;
            }

            if (Variable.Taskbars.IsMouseOverTaskbar()) return;

            // todo may have a bug here
            EnumWindows((handle, lp) =>
            {
                if (handle.IsWindowInvisible()) return true;

                if (handle.IsNotMaximizeWindow()) return true;

                if (lp.ModeType == AutoModeType.BlacklistMode && handle.InBlacklist(lp))
                    return true;

                if (lp.ModeType == AutoModeType.WhitelistMode && handle.NotInWhitelist(lp))
                    return true;

                _maxWindow = handle;
                return false;
            }, in _userSettings);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;

                if (_userSettings.ModeType == AutoModeType.BlacklistMode)
                {
                    _userSettings.BlistDefaultState.SetState();
                    return;
                }

                if (_userSettings.ModeType == AutoModeType.WhitelistMode)
                {
                    _userSettings.WlistDefaultState.SetState();
                    return;
                }

                return;
            }

            if (_userSettings.ModeType == AutoModeType.BlacklistMode)
            {
                _userSettings.BlistTargetState.SetState();
                return;
            }

            if (_userSettings.ModeType == AutoModeType.WhitelistMode) _userSettings.WlistTargetState.SetState();
        }
    }
}