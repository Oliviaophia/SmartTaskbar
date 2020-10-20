using System;
using System.Drawing;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;
using SmartTaskbar.PlatformInvoke;

namespace SmartTaskbar.Tray.ViewModels
{
    public class MainNotifyIconViewModel : IUserConfiguration
    {
        public Icon Icon
            => IconStyle switch
            {
                IconStyle.Auto => UIInfo.IsSystemUsesLightTheme()
                    ? IconResource.Logo_Black
                    : IconResource.Logo_White,
                IconStyle.Black => IconResource.Logo_Black,
                IconStyle.Blue  => IconResource.Logo_Blue,
                IconStyle.Pink  => IconResource.Logo_Pink,
                IconStyle.White => IconResource.Logo_White,
                _               => throw new NotImplementedException()
            };

        public IconStyle IconStyle { get; set; }
    }
}
