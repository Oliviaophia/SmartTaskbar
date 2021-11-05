using Windows.UI.ViewManagement;
using Microsoft.Win32;

namespace SmartTaskbar;

public static class UISettingsHelper
{
    public static readonly UISettings Settings = new();

    /// <summary>
    ///     Determine whether it is a light theme
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool IsLightTheme()
    {
        using var personalizeKey =
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false)
            ?? throw new InvalidOperationException("OpenSubKey Personalize Failed.");

        return (int) (personalizeKey.GetValue("SystemUsesLightTheme", 0) ?? 0) == 1;
    }
}
