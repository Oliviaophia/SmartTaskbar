namespace SmartTaskbar;

/// <summary>
///     User settings configuration
/// </summary>
internal struct UserConfiguration
{
    /// <summary>
    ///     Auto mode type
    /// </summary>
    public AutoModeType AutoModeType { get; set; }

    /// <summary>
    ///     Show taskbar when exiting
    /// </summary>
    public bool ShowTaskbarWhenExit { get; set; }
}
