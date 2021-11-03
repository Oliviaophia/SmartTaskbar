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

    /// <summary>
    ///     When the mouse is moved to the left side of the taskbar,
    ///     the taskbar icons are automatically aligned to the left
    /// </summary>
    public bool AlignLeftWhenTheMouseIsLeft { get; set; }
}
