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
    ///     Reverse display mode behavior
    /// </summary>
    public bool ReverseDisplayModeBehavior { get; set; }

    /// <summary>
    ///     Reverse size mode behavior
    /// </summary>
    public bool ReverseSizeModeBehavior { get; set; }

    /// <summary>
    ///     Pause Auto Mode in Tablet Mode
    /// </summary>
    public bool PauseInTabletMode { get; set; }

    /// <summary>
    ///     Show taskbar when exiting
    /// </summary>
    public bool ShowTaskbarWhenExit { get; set; }
}
