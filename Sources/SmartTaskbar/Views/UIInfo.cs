using Microsoft.Win32;
using Windows.UI.ViewManagement;

namespace SmartTaskbar
{
    public static class UIInfo
    {
        public static readonly UISettings Settings = new();

        private static readonly RegistryKey Key =
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false)
            ?? throw new InvalidOperationException("OpenSubKey Failed.");

        public static bool IsLightTheme()
            => (int)(Key.GetValue("SystemUsesLightTheme", 0) ?? 0) == 1;
    }
}
