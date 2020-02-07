using Microsoft.Win32;

namespace SmartTaskbar.Core.Helpers
{
    internal static class LightTheme
    {
        private static readonly RegistryKey Key =
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);

        internal static bool IsSystemUsesLightTheme() => (int) Key.GetValue("SystemUsesLightTheme", 0) == 1;
    }
}