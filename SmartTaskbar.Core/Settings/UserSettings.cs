using System.Collections.Generic;

namespace SmartTaskbar.Core.Settings
{
    internal static class UserSettings
    {
        internal static AutoModeType modeType;

        internal static bool autoHide;

        internal static bool buttonSize;

        internal static HashSet<string> blacklist;

        internal static HashSet<string> whitelist;

        internal static TransparentModeType transparentType;

        internal static bool win10;
    }
}