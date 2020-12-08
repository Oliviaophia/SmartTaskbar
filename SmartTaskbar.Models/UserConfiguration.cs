namespace SmartTaskbar.Models
{
    public record UserConfiguration
    {
        public IconStyle IconStyle { get; init; } = IconStyle.Auto;

        public AutoModeType AutoModeType { get; init; } = AutoModeType.AutoHideApiMode;
    }
}
