using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.UI.ViewModels
{
    public class MainContextMenuViewModel : IUserConfiguration
    {
        public IconStyle IconStyle { get; set; }
        public AutoModeType AutoModeType { get; set; }
    }
}
