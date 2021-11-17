using System;
using Microsoft.Win32;

namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const int SmallIcon = 1;
        private const int BigIcon = 0;
        private const string TaskbarSmallIcons = "TaskbarSmallIcons";

        private static RegistryKey GetAdvancedKey()
            => Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);

        /// <summary>
        ///     Set to use the larger taskbar icon
        /// </summary>
        public static void SetBigIcon()
        {
            using (var key = GetAdvancedKey()) { key.SetValue(TaskbarSmallIcons, BigIcon); }

            BroadcastTraySettings();
        }

        /// <summary>
        ///     Set to use the small taskbar icon
        /// </summary>
        public static void SetSmallIcon()
        {
            using (var key = GetAdvancedKey()) { key.SetValue(TaskbarSmallIcons, SmallIcon); }

            BroadcastTraySettings();
        }

        /// <summary>
        ///     Determine whether is use the small taskbar icon
        /// </summary>
        public static bool IsUseSmallIcon()
        {
            using (var key = GetAdvancedKey()) { return (int)key.GetValue("TaskbarSmallIcons", BigIcon) == SmallIcon; }
        }

        /// <summary>
        ///     Change the Taskbar buttons size
        /// </summary>
        public static void ChangeIconSize()
        {
            using (var key = GetAdvancedKey())
            {
                key.SetValue(TaskbarSmallIcons,
                             (int)key.GetValue("TaskbarSmallIcons", BigIcon) == SmallIcon ? BigIcon : SmallIcon);
            }

            BroadcastTraySettings();
        }

        /// <summary>
        ///     Broadcast settings change
        /// </summary>
        private static void BroadcastTraySettings()
            => SendNotifyMessage((IntPtr)0xffff, 0x001a, (UIntPtr)0, "TraySettings");
    }
}
