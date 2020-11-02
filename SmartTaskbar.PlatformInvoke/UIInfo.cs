using Windows.UI;
using Windows.UI.ViewManagement;
using Microsoft.Win32;
using DrawingColor = System.Drawing.Color;

namespace SmartTaskbar.PlatformInvoke
{
    public static class UIInfo
    {
        private static readonly UISettings Settings = new UISettings();

        private static readonly RegistryKey Key =
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);

        // https://stackoverflow.com/questions/51334674/how-to-detect-windows-10-light-dark-mode-in-win32-application
        public static DrawingColor ForeGround
            => Settings.GetColorValue(UIColorType.Foreground).ToColor();

        public static DrawingColor Background
            => Settings.GetColorValue(UIColorType.Background).ToColor();

        public static DrawingColor Accent
            => Settings.GetColorValue(UIColorType.Accent).ToColor();

        public static DrawingColor AccentDark1
            => Settings.GetColorValue(UIColorType.AccentDark1).ToColor();

        public static DrawingColor AccentDark2
            => Settings.GetColorValue(UIColorType.AccentDark2).ToColor();

        public static DrawingColor AccentDark3
            => Settings.GetColorValue(UIColorType.AccentDark3).ToColor();

        public static DrawingColor AccentLight1
            => Settings.GetColorValue(UIColorType.AccentLight1).ToColor();

        public static DrawingColor AccentLight2
            => Settings.GetColorValue(UIColorType.AccentLight2).ToColor();

        public static DrawingColor AccentLight3
            => Settings.GetColorValue(UIColorType.AccentLight3).ToColor();

        public static bool IsLightTheme()
            => (int) Key.GetValue("SystemUsesLightTheme", 0)! == 1;

        private static DrawingColor ToColor(this Color color)
            => DrawingColor.FromArgb(color.A, color.R, color.G, color.B);
    }
}
