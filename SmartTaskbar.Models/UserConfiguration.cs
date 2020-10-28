using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.Models
{
    public class UserConfiguration : IUserConfiguration
    {
        public IconStyle IconStyle { get; set; } = IconStyle.Auto;

        public AutoModeType AutoModeType { get; set; } = AutoModeType.AutoHideApiMode;
    }
}
