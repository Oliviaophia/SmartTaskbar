using System;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class AutoMode : IAutoMode
    {
        private static IntPtr maxWindow;
        private static bool tryShowBar;

        public AutoMode()
        {
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            ResetValue();
            taskbars.UpdateTaskbarList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            if (maxWindow == IntPtr.Zero)
            {
                if (taskbars.IsMouseOverTaskbar())
                {
                    return;
                }

                EnumWindows((handle, lp) =>
                {
                    if (handle.IsWindowInvisible())
                    {
                        return true;
                    }

                    if (handle.IsMaximizeWindow())
                    {
                        if (UserSettings.ModeType == AutoModeType.BlacklistMode)
                        {
                            // todo 
                        }

                        if (UserSettings.ModeType == AutoModeType.WhitelistMode)
                        {
                            // todo
                        }

                        maxWindow = handle;
                        return false;
                    }

                    return true;
                }, IntPtr.Zero);

                if (maxWindow == IntPtr.Zero)
                {
                    if (tryShowBar == false)
                    {
                        return;
                    }
                    tryShowBar = false;
                    if (UserSettings.ModeType == AutoModeType.OldAutoMode)
                    {
                        AutoHide.CancelAutoHide();
                        return;
                    }
                    if (UserSettings.ModeType == AutoModeType.OldAdaptiveMode)
                    {
                        ButtonSize.SetIconSize(Constant.Iconlarge);
                    }
                    return;
                }
                if (UserSettings.ModeType == AutoModeType.OldAutoMode)
                {
                    AutoHide.SetAutoHide();
                    return;
                }
                if (UserSettings.ModeType == AutoModeType.OldAdaptiveMode)
                {
                    ButtonSize.SetIconSize(Constant.IconSmall);
                }
            }
            else
            {
                if (maxWindow.IsWindowInvisible())
                {
                    ResetValue();
                    return;
                }

                if (maxWindow.IsMaximizeWindow())
                {
                    return;
                }
                ResetValue();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ResetValue()
        {
            maxWindow = IntPtr.Zero;
            tryShowBar = true;
        }
    }
}
