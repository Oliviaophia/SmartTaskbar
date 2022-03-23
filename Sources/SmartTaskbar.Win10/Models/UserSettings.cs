using SmartTaskbar.Models;
using SmartTaskbar.Properties;

namespace SmartTaskbar
{
    internal class UserSettings
    {
        private static UserConfiguration _userConfiguration;

        /// <summary>
        ///     ctor
        /// </summary>
        static UserSettings()
            => _userConfiguration = new UserConfiguration
            {
                AutoModeType = Settings.Default.AutoModeType.AsAutoModeType(),
                ShowTaskbarWhenExit = Settings.Default.ShowTaskbarWhenExit,
                ReverseSizeModeBehavior = Settings.Default.ReverseSizeModeBehavior,
                ReverseDisplayModeBehavior = Settings.Default.ReverseDisplayModeBehavior,
                PauseInTabletMode = Settings.Default.PauseInTabletMode
            };

        public static AutoModeType AutoModeType
        {
            get => _userConfiguration.AutoModeType;
            set
            {
                if (value == _userConfiguration.AutoModeType)
                    return;

                _userConfiguration.AutoModeType = value;
                Settings.Default.AutoModeType = value.ToString();
                Settings.Default.Save();
            }
        }

        public static bool ReverseDisplayModeBehavior
        {
            get => _userConfiguration.ReverseDisplayModeBehavior;
            set
            {
                if (value == _userConfiguration.ReverseDisplayModeBehavior)
                    return;

                _userConfiguration.ReverseDisplayModeBehavior = value;
                Settings.Default.ReverseDisplayModeBehavior = value;
                Settings.Default.Save();
            }
        }

        public static bool ReverseSizeModeBehavior
        {
            get => _userConfiguration.ReverseSizeModeBehavior;
            set
            {
                if (value == _userConfiguration.ReverseSizeModeBehavior)
                    return;

                _userConfiguration.ReverseSizeModeBehavior = value;
                Settings.Default.ReverseSizeModeBehavior = value;
                Settings.Default.Save();
            }
        }

        public static bool PauseInTabletMode
        {
            get => _userConfiguration.PauseInTabletMode;
            set
            {
                if (value == _userConfiguration.PauseInTabletMode)
                    return;

                _userConfiguration.PauseInTabletMode = value;
                Settings.Default.PauseInTabletMode = value;
                Settings.Default.Save();
            }
        }

        public static bool ShowTaskbarWhenExit
        {
            get => _userConfiguration.ShowTaskbarWhenExit;
            set
            {
                if (value == _userConfiguration.ShowTaskbarWhenExit)
                    return;

                _userConfiguration.ShowTaskbarWhenExit = value;
                Settings.Default.ShowTaskbarWhenExit = value;
                Settings.Default.Save();
            }
        }
    }
}
