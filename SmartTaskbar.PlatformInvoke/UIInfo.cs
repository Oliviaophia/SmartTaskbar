
using Windows.UI;
using Windows.UI.ViewManagement;
using Microsoft.Win32;

namespace SmartTaskbar.PlatformInvoke
{
    public static class UIInfo
    {
        private static readonly Color White = Color.FromArgb(255, 255, 255, 255);
        private static readonly UISettings Settings = new UISettings();

        private static readonly RegistryKey Key =
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);

        public static bool IsSystemUsesLightTheme()
            => (int) Key.GetValue("SystemUsesLightTheme", 0)! == 1;

        // https://stackoverflow.com/questions/51334674/how-to-detect-windows-10-light-dark-mode-in-win32-application
        public static Color ForeGround
            => Settings.GetColorValue(UIColorType.Foreground);

        public static Color Background
            => Settings.GetColorValue(UIColorType.Background);

        public static Color Accent
            => Settings.GetColorValue(UIColorType.Accent);

        public static Color AccentDark1
            => Settings.GetColorValue(UIColorType.AccentDark1);

        public static Color AccentDark2
            => Settings.GetColorValue(UIColorType.AccentDark2);

        public static Color AccentDark3
            => Settings.GetColorValue(UIColorType.AccentDark3);

        public static Color AccentLight1
            => Settings.GetColorValue(UIColorType.AccentLight1);

        public static Color AccentLight2
            => Settings.GetColorValue(UIColorType.AccentLight2);

        public static Color AccentLight3
            => Settings.GetColorValue(UIColorType.AccentLight3);
    }
}
