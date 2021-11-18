using System;
using Windows.UI.ViewManagement;
using Microsoft.Win32;

namespace SmartTaskbar
{
    public static partial class Fun
    {
        public static readonly UISettings UISettings = new UISettings();

        /// <summary>
        ///     Determine whether it is a light theme
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool IsLightTheme()
        {
            using (var personalizeKey =
                   Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                                                   false))
            {
                return (int)(personalizeKey?.GetValue("SystemUsesLightTheme", 0) ?? 0) == 1;
            }
        }

        /// <summary>
        ///     Determine whether it is tablet mode
        /// </summary>
        /// <returns></returns>
        public static bool IsTabletMode()
        {
            switch (Registry.CurrentUser.GetValue(
                        @"Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\TabletMode",
                        0))
            {
                case 1:
                    return true;
                case 0:
                    return false;
                default:
                    throw new Exception();
            }
        }
    }
}
