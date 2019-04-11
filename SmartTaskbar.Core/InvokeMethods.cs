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
        }

        #endregion

        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region UpdateCache

        public static void UpdateCache()
        {
            taskbars.UpdateTaskbarList();
            nameCache.UpdateCacheName();
        }

        #endregion

        #region Config

        private static readonly Lazy<UserSettings> Instance = new Lazy<UserSettings>(() => new UserSettings());

        public static UserSettings Settings => Instance.Value;

        public static void GetUserConfig() => SettingsHelper.ReadSettings();

        public static void SaveUserConfig() => SettingsHelper.SaveSettings();

        #endregion
    }
}