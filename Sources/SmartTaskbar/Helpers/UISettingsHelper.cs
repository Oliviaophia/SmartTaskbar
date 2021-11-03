using Microsoft.Win32;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

public static class UISettingsHelper
{
    private static readonly IntPtr HwndBroadcast = new (0xffff);
    private const int WmSettingChange = 0x001a;

    public static readonly Windows.UI.ViewManagement.UISettings Settings = new();

    /// <summary>
    ///     Get CurrentUser Advanced Registry Key
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static RegistryKey GetAdvancedKey()
        => Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true)
        ?? throw new InvalidOperationException("OpenSubKey Advanced Failed.");

    /// <summary>
    ///     Determine whether it is a light theme
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool IsLightTheme()
    {
        using var personalizeKey =
        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false) ?? throw new InvalidOperationException("OpenSubKey Personalize Failed.");

        return (int)(personalizeKey.GetValue("SystemUsesLightTheme", 0) ?? 0) == 1;
    }

    /// <summary>
    ///     Determine whether the taskbar icon is aligned in the center
    /// </summary>
    /// <returns></returns>
    public static bool IsCenterAlignment()
    {
        using var advancedKey = GetAdvancedKey();

        return (int)(advancedKey.GetValue("TaskbarAl", 1) ?? 1) == 1;
    }

    /// <summary>
    ///     Set left-aligned taskbar icons
    /// </summary>
    public static void SetLeftAlignment()
    {
        using var advancedKey = GetAdvancedKey();

        advancedKey.SetValue("TaskbarAl", 0); 
        BroadcastSystemChange();
    }

    /// <summary>
    ///     Set the center alignment of the taskbar icon
    /// </summary>
    public static void SetCenterAlignment()
    {
        using var advancedKey = GetAdvancedKey();

        advancedKey.SetValue("TaskbarAl", 1);
        BroadcastSystemChange();
    }

    /// <summary>
    ///     Broadcast messages to update the system UI
    /// </summary>
    private static void BroadcastSystemChange()
        => SendNotifyMessage(HwndBroadcast, WmSettingChange, UIntPtr.Zero, "TraySettings");
}
