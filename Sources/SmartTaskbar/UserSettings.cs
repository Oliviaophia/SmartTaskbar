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
        var autoMode = ApplicationData.Current.LocalSettings.Values[nameof(UserConfiguration.AutoModeType)] as string;

        _userConfiguration = new UserConfiguration
        {
            AutoModeType = autoMode == nameof(AutoModeType.None) ? AutoModeType.None : AutoModeType.Auto,
            ShowTaskbarWhenExit =
                ApplicationData.Current.LocalSettings.Values[nameof(UserConfiguration.ShowTaskbarWhenExit)] as bool?
                ?? true,
            AlignLeftWhenTheMouseIsLeft =
                ApplicationData.Current.LocalSettings.Values[nameof(UserConfiguration.AlignLeftWhenTheMouseIsLeft)] as
                    bool?
                ?? false
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
            ApplicationData.Current.LocalSettings.Values[nameof(UserConfiguration.AutoModeType)] = value.ToString();
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
            ApplicationData.Current.LocalSettings.Values[nameof(UserConfiguration.ShowTaskbarWhenExit)] = value;
        }
    }

    public static bool AlignLeftWhenTheMouseIsLeft
    {
        get => _userConfiguration.AlignLeftWhenTheMouseIsLeft;
        set
        {
            if (value == _userConfiguration.AlignLeftWhenTheMouseIsLeft)
                return;

            _userConfiguration.AlignLeftWhenTheMouseIsLeft = value;
            ApplicationData.Current.LocalSettings.Values[nameof(UserConfiguration.AlignLeftWhenTheMouseIsLeft)] = value;
        }
    }

    public event EventHandler<AutoModeType>? OnAutoModeTypePropertyChanged;
}
