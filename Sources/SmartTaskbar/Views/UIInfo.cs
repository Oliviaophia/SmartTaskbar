using Windows.UI.ViewManagement;
using Microsoft.Win32;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

public static class UiInfo
{
    private static readonly IntPtr HwndBroadcast = new (0xffff);
    private const int WmSettingChange = 0x001a;

    public static readonly UISettings Settings = new();

    private static RegistryKey GetAdvancedKey()
        => Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true)
        ?? throw new InvalidOperationException("OpenSubKey Advanced Failed.");

    public static bool IsLightTheme()
    {
        using var personalizeKey =
        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false) ?? throw new InvalidOperationException("OpenSubKey Personalize Failed.");

        return (int)(personalizeKey.GetValue("SystemUsesLightTheme", 0) ?? 0) == 1;
    }

    public static bool IsCenterAlignment()
    {
        using var advancedKey = GetAdvancedKey();

        return (int)(advancedKey.GetValue("TaskbarAl", 1) ?? 1) == 1;
    }

    public static void SetLeftAlignment()
    {
        using var advancedKey = GetAdvancedKey();

        advancedKey.SetValue("TaskbarAl", 0); 
        BroadcastSystemChange();
    }

    public static void SetCenterAlignment()
    {
        using var advancedKey = GetAdvancedKey();

        advancedKey.SetValue("TaskbarAl", 1);
        BroadcastSystemChange();
    }

    private static void BroadcastSystemChange()
        => SendNotifyMessage(HwndBroadcast, WmSettingChange, UIntPtr.Zero, "TraySettings");
}
