using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ButtonSize
    {
        private const int HwndBroadcast = 0xffff;
        private const int WmSettingChange = 0x001a;

        private static readonly RegistryKey Key =
            Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIconSize(int size)
        {
            Key.SetValue("TaskbarSmallIcons", size);
            // https://github.com/cprcrack/AdaptiveTaskbar/blob/4a1ce94044ae3de47ba63877558794dd698ad9e5/Program.cs#L165
            SendNotifyMessage((IntPtr) HwndBroadcast, WmSettingChange, UIntPtr.Zero, "TraySettings");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIconSize() => (int) Key.GetValue("TaskbarSmallIcons", Constant.Iconlarge);
    }
}