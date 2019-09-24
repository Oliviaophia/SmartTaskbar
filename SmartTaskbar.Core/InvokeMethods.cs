using System;
using SmartTaskbar.Core.AutoMode;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region ctor

        static InvokeMethods()
        {
            UpdateCache();
            GetUserConfig();
            SaveUserConfig();
            SetHook();
        }

        #endregion

        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id)
        {
            PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);
        }

        #endregion

        #region Update

        public static void UpdateCache()
        {
            Variable.NameCache.UpdateCacheName();
            Variable.Taskbars.ResetTaskbars();
            SetHook();
        }

        #endregion

        #region Config

        private static readonly Lazy<UserSettings> Instance = new Lazy<UserSettings>(() => new UserSettings());

        public static UserSettings UserConfig => Instance.Value;

        public static void GetUserConfig() => SettingsHelper.ReadSettings();

        public static void SaveUserConfig() => SettingsHelper.SaveSettings();

        public static void SetTransparent() => Variable.Taskbars.TransparentBar();

        public static void SetHook() => HookBar.SetHook();

        public static bool IsLightTheme() => LightTheme.IsSystemUsesLightTheme();

        #endregion

        #region AutoMode

        public static void AutoModeRun() => AutoModeController.Run();

        public static void AutoModeReady() => AutoModeController.Ready();

        public static void AutoModeSet(AutoModeType modeType) => AutoModeController.SetMode(modeType);

        #endregion
    }
}