namespace SmartTaskbar.Models
{
    public record TaskbarState(bool                IsAutoHide,
                               bool                HideTaskbarCompletely,
                               IconSize            IconSize,
                               TransparentModeType TransparentMode);
}
