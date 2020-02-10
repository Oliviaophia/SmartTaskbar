using System;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region Config

        public static UserSettings GetUserSettings() => SettingsHelper.ReadSettings();

        public static void SaveUserSettings(in UserSettings userSettings) => SettingsHelper.SaveSettings(userSettings);

        public static bool IsLightTheme() => LightTheme.IsSystemUsesLightTheme();

        public static void ResetAutoModeState(in UserSettings userSettings) =>
            Variable.Taskbars.SetBarState(userSettings.ResetState);

        #endregion
    }
}