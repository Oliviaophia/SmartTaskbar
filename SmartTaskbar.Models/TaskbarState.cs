namespace SmartTaskbar.Models
{
    public record TaskbarState(bool                IsAutoHide,
                               bool                HideTaskbarCompletely,
                               int                 IconSize,
                               TransparentModeType TransparentMode);
}
