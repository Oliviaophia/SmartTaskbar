using System;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.UserConfig;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class AutoMode : IAutoMode
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AutoMode()
        {
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _maxWindow = IntPtr.Zero;
            _tryShowBar = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            if (_maxWindow != IntPtr.Zero)
            {
                if (_maxWindow.IsWindowInvisible() || _maxWindow.IsNotMaximizeWindow()) Reset();
                return;
            }

            if (taskbars.IsMouseOverTaskbar()) return;

            EnumWindows((handle, lp) =>
            {
                if (handle.IsWindowInvisible()) return true;

                if (handle.IsNotMaximizeWindow()) return true;

                switch (UserSettings.Get.ModeType)
                {
                    case AutoModeType.BlacklistMode when handle.InBlacklist():
                        return true;
                    case AutoModeType.WhitelistMode when handle.NotInWhitelist():
                        return true;
                    default:
                        _maxWindow = handle;
                        return false;
                }
            }, IntPtr.Zero);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;
                switch (UserSettings.Get.ModeType)
                {
                    case AutoModeType.ClassicAutoMode:
                        AutoHide.CancelAutoHide();
                        return;
                    case AutoModeType.ClassicAdaptiveMode:
                        ButtonSize.SetIconSize(Constant.Iconlarge);
                        return;
                    default:
                        return;
                }
            }

            switch (UserSettings.Get.ModeType)
            {
                case AutoModeType.ClassicAutoMode:
                    AutoHide.SetAutoHide();
                    return;
                case AutoModeType.ClassicAdaptiveMode:
                    ButtonSize.SetIconSize(Constant.IconSmall);
                    break;
            }
        }
    }
}