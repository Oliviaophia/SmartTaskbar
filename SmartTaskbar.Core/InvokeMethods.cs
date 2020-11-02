using System;
using System.Threading.Tasks;
using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id)
            => PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region Config

        public static async Task<UserSettings> GetUserSettings()
            => await SettingsHelper.ReadSettingsAsync();

        public static async void SaveUserSettings(UserSettings userSettings)
            => await SettingsHelper.SaveSettingsAsync(userSettings);

        public static bool IsLightTheme()
            => LightTheme.IsSystemUsesLightTheme();

        public static void ResetAutoModeState(in UserSettings userSettings)
            => Variable.Taskbars.SetBarState(userSettings.ResetState);

        public static bool GetTaskbarAnimation()
            => Animation.GetTaskbarAnimation();

        public static void ChangeTaskbarAnimation()
            => Animation.ChangeTaskbarAnimation();

        #endregion
    }
}
