using System;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class AutoMode : IAutoMode
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;

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
            if (_maxWindow == IntPtr.Zero)
            {
                if (taskbars.IsMouseOverTaskbar()) return;

                EnumWindows((handle, lp) =>
                {
                    if (handle.IsWindowInvisible()) return true;

                    if (handle.IsMaximizeWindow())
                    {
                        if (UserSettings.modeType == AutoModeType.BlacklistMode)
                        {
                            
                        }

                        if (UserSettings.modeType == AutoModeType.WhitelistMode)
                        {
                            // todo
                        }

                        _maxWindow = handle;
                        return false;
                    }

                    return true;
                }, IntPtr.Zero);

                if (_maxWindow == IntPtr.Zero)
                {
                    if (_tryShowBar == false) return;
                    _tryShowBar = false;
                    if (UserSettings.modeType == AutoModeType.ClassicAutoMode)
                    {
                        AutoHide.CancelAutoHide();
                        return;
                    }

                    if (UserSettings.modeType == AutoModeType.ClassicAdaptiveMode)
                    {
                        ButtonSize.SetIconSize(Constant.Iconlarge);
                    }
                    return;
                }

                if (UserSettings.modeType == AutoModeType.ClassicAutoMode)
                {
                    AutoHide.SetAutoHide();
                    return;
                }

                if (UserSettings.modeType == AutoModeType.ClassicAdaptiveMode)
                {
                    ButtonSize.SetIconSize(Constant.IconSmall);
                }
            }
            else
            {
                if (_maxWindow.IsWindowInvisible())
                {
                    Reset();
                    return;
                }

                if (_maxWindow.IsMaximizeWindow()) return;
                Reset();
            }
        }
    }
}