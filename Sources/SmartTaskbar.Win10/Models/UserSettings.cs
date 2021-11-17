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

        public event EventHandler<AutoModeType> OnAutoModeTypePropertyChanged;
    }
}
