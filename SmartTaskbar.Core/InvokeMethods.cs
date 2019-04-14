using System;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.UserConfig;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region ctor

        public static void Initialization()
        {
            UpdateCache();
            GetUserConfig();
            SaveUserConfig();
            SetHook();
        }

        #endregion

        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region Update

        public static void UpdateCache()
        {
            Variable.nameCache.UpdateCacheName();
            Variable.taskbars.ResetTaskbars();
            SetHook();
        }

        #endregion

        #region Config

        private static readonly Lazy<UserSettings> Instance = new Lazy<UserSettings>(() => new UserSettings());

        public static UserSettings Settings => Instance.Value;

        public static void GetUserConfig() => SettingsHelper.ReadSettings();

        public static void SaveUserConfig() => SettingsHelper.SaveSettings();

        public static void SetTransparent() => Variable.taskbars.TransparentBar();

        public static void SetHook() => HookBar.SetHook();

        #endregion
    }
}