namespace SmartTaskbar.Models
{
    public class UserConfiguration
    {
        public IconStyle IconStyle { get; set; } = IconStyle.Auto;

        public AutoModeType AutoModeType { get; set; } = AutoModeType.AutoHideApiMode;
    }
}
