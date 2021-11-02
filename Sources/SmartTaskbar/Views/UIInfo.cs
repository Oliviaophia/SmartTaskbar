using Windows.UI.ViewManagement;
using Microsoft.Win32;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

public static class UiInfo
{
    private static readonly IntPtr HwndBroadcast = new (0xffff);
    private const int WmSettingChange = 0x001a;

    public static readonly UISettings Settings = new();

    private static readonly RegistryKey PersonalizeKey =
        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false)
        ?? throw new InvalidOperationException("OpenSubKey Failed.");

    private static readonly RegistryKey AdvancedKey =
        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true)
        ?? throw new InvalidOperationException("OpenSubKey Failed.");

    public static bool IsLightTheme()
        => (int) (PersonalizeKey.GetValue("SystemUsesLightTheme", 0) ?? 0) == 1;

    public static bool IsCenterAlignment()
        => (int) (AdvancedKey.GetValue("TaskbarAl", 1) ?? 1) == 1;

    public static void SetLeftAlignment()
    {
        AdvancedKey.SetValue("TaskbarAl", 0); 
        BroadcastSystemChange();
    }

    public static void SetCenterAlignment()
    {
        AdvancedKey.SetValue("TaskbarAl", 1);
        BroadcastSystemChange();
    }

    private static void BroadcastSystemChange()
        => SendNotifyMessage(HwndBroadcast, WmSettingChange, UIntPtr.Zero, "TraySettings");
}
