using Microsoft.Win32;

namespace SmartTaskbar.PlatformInvoke
{
    public static class LightTheme
    {
        private static readonly RegistryKey Key =
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);

        public static bool IsSystemUsesLightTheme()
            => (int) Key.GetValue("SystemUsesLightTheme", 0)! == 1;
    }
}
