using Windows.Storage;

namespace SmartTaskbar;

internal class UserSettings
{
    private static UserConfiguration _userConfiguration;

    /// <summary>
    ///     ctor
    /// </summary>
    public UserSettings()
    {
        var autoMode = ApplicationData.Current.LocalSettings.Values["AutoModeType"] as string;

        _userConfiguration = new UserConfiguration
        {
            AutoModeType = autoMode == nameof(AutoModeType.None) ? AutoModeType.None : AutoModeType.Auto,
            ShowTaskbarWhenExit =
                ApplicationData.Current.LocalSettings.Values["ShowTaskbarWhenExit"] as bool? ?? true
        };
    }

    public AutoModeType AutoModeType
    {
        get => _userConfiguration.AutoModeType;
        set
        {
            if (value == _userConfiguration.AutoModeType)
                return;

            _userConfiguration.AutoModeType = value;
            ApplicationData.Current.LocalSettings.Values["AutoModeType"] = value.ToString();
            OnAutoModeTypePropertyChanged?.Invoke(null, value);
        }
    }

    public bool ShowTaskbarWhenExit
    {
        get => _userConfiguration.ShowTaskbarWhenExit;
        set
        {
            if (value == _userConfiguration.ShowTaskbarWhenExit)
                return;

            _userConfiguration.ShowTaskbarWhenExit = value;
            ApplicationData.Current.LocalSettings.Values["ShowTaskbarWhenExit"] = value;
        }
    }

    public event EventHandler<AutoModeType>? OnAutoModeTypePropertyChanged;
}
