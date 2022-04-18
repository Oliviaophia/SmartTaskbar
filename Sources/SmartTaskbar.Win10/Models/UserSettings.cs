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
                ReduceTaskbarDisplay = Settings.Default.ReduceTaskbarDisplay
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

        public static bool ReduceTaskbarDisplay
        {
            get => _userConfiguration.ReduceTaskbarDisplay;
            set
            {
                if (value == _userConfiguration.ReduceTaskbarDisplay)
                    return;

                _userConfiguration.ReduceTaskbarDisplay = value;
                Settings.Default.ReduceTaskbarDisplay = value;
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
