using Microsoft.Win32;
using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ButtonSize
    {
        private static readonly RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
        private const int HWND_BROADCAST = 0xffff;
        private const int WM_SETTINGCHANGE = 0x001a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIconSize(int size)
        {
            Key.SetValue("TaskbarSmallIcons", size);
            // https://github.com/cprcrack/AdaptiveTaskbar/blob/4a1ce94044ae3de47ba63877558794dd698ad9e5/Program.cs#L165
            SendNotifyMessage((IntPtr)HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, "TraySettings");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIconSize() => (int)Key.GetValue("TaskbarSmallIcons", Constant.Iconlarge);
    }
}
