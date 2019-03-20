using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SmartTaskbar.Core.UserConfig
{
    [Serializable]
    public class UserSettings
    {
        private static readonly Lazy<UserSettings> Instance = new Lazy<UserSettings>(() => new UserSettings());

        public static UserSettings Get => Instance.Value;

        public AutoModeType ModeType { get; set; }

        public bool AutoHide { get; set; }

        public bool SmallButton { get; set; }

        public HashSet<string> Blacklist { get; set; }

        public HashSet<string> Whitelist { get; set; }

        public TransparentModeType TransparentType { get; set; }

        public bool HideTaskbarCompletely { get; set; }

        public string Language { get; set; }

        public bool CenteredIcon { get; set; }

        public bool DisabledOnTabletMode { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateSettings(UserSettings settings)
        {
            if (settings is null)
            {
                ModeType = AutoModeType.ForegroundMode;
                AutoHide = true;
                SmallButton = Helpers.ButtonSize.GetIconSize() == Constant.IconSmall;
                Blacklist = new HashSet<string>(16);
                Whitelist = new HashSet<string>(16);
                TransparentType = TransparentModeType.Disabled;
                HideTaskbarCompletely = false;
                CenteredIcon = false;
                DisabledOnTabletMode = false;
                return;
            }

            ModeType = settings.ModeType;
            AutoHide = settings.AutoHide;
            SmallButton = settings.SmallButton;
            Blacklist = settings.Blacklist;
            Whitelist = settings.Whitelist;
            TransparentType = settings.TransparentType;
            HideTaskbarCompletely = settings.HideTaskbarCompletely;
            Language = settings.Language;
            CenteredIcon = settings.CenteredIcon;
            DisabledOnTabletMode = settings.DisabledOnTabletMode;
        }
    }
}