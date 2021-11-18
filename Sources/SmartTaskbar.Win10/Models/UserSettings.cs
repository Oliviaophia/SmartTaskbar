using System;
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
        public UserSettings()
            => _userConfiguration = new UserConfiguration
            {
                AutoModeType = Settings.Default.AutoModeType.AsAutoModeType(),
                ShowTaskbarWhenExit = Settings.Default.ShowTaskbarWhenExit
            };

        public AutoModeType AutoModeType
        {
            get => _userConfiguration.AutoModeType;
            set
            {
                if (value == _userConfiguration.AutoModeType)
                    return;

                _userConfiguration.AutoModeType = value;
                Settings.Default.AutoModeType = value.ToString();
                OnAutoModeTypePropertyChanged?.Invoke(null, value);
            }
        }

        public event EventHandler<AutoModeType> OnAutoModeTypePropertyChanged;

        public static bool ReverseDisplayModeBehavior
        {
            get => _userConfiguration.ReverseDisplayModeBehavior;
            set
            {
                if (value == _userConfiguration.ReverseDisplayModeBehavior)
                    return;

                _userConfiguration.ReverseDisplayModeBehavior = value;
                Settings.Default.ReverseDisplayModeBehavior = value;
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
            }
        }
    }
}
