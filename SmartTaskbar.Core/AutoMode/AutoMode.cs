using System;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;
using static SmartTaskbar.Core.InvokeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class AutoMode : IAutoMode
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoMode()
        {
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

            EnumWindows((handle, lp) =>
            {
                if (handle.IsWindowInvisible()) return true;

                if (handle.IsNotMaximizeWindow()) return true;

                switch (lp)
                {
                    case AutoModeType.BlacklistMode when handle.InBlacklist():
                        return true;
                    case AutoModeType.WhitelistMode when handle.NotInWhitelist():
                        return true;
                    default:
                        _maxWindow = handle;
                        return false;
                }
            }, UserConfig.ModeType);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;
                switch (UserConfig.ModeType)
                {
                    case AutoModeType.BlacklistMode:
                        UserConfig.BlistDefaultState.SetState();
                        return;
                    case AutoModeType.WhitelistMode:
                        UserConfig.WlistDefaultState.SetState();
                        return;
                    default:
                        return;
                }
            }

            switch (UserConfig.ModeType)
            {
                case AutoModeType.BlacklistMode:
                    UserConfig.BlistTargetState.SetState();
                    return;
                case AutoModeType.WhitelistMode:
                    UserConfig.WlistTargetState.SetState();
                    return;
            }
        }
    }
}