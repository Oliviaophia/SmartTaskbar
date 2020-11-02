using System;
using System.Drawing;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;
using SmartTaskbar.PlatformInvoke;

namespace SmartTaskbar.UI.ViewModels
{
    public class MainNotifyIconViewModel : IUserConfiguration
    {
        public Icon Icon
            => IconStyle switch
            {
                IconStyle.Auto => UIInfo.IsLightTheme()
                    ? IconResources.Logo_Black
                    : IconResources.Logo_White,
                IconStyle.Black => IconResources.Logo_Black,
                IconStyle.Blue  => IconResources.Logo_Blue,
                IconStyle.Pink  => IconResources.Logo_Pink,
                IconStyle.White => IconResources.Logo_White,
                _               => throw new ArgumentOutOfRangeException()
            };

        public IconStyle IconStyle { get; set; }
        public AutoModeType AutoModeType { get; set; }
    }
}
