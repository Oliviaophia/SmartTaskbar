using System;
using System.Drawing;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;
using SmartTaskbar.PlatformInvoke;

namespace SmartTaskbar.Tray.ViewModels
{
    public class MainNotifyIconViewModel : IUserConfiguration
    {
        public IconStyle IconStyle { get; set; }

        public Icon Icon
            => IconStyle switch
            {
                IconStyle.Auto => LightTheme.IsSystemUsesLightTheme()
                    ? IconResource.Logo_Black
                    : IconResource.Logo_White,
                IconStyle.Black => IconResource.Logo_Black,
                IconStyle.Blue  => IconResource.Logo_Blue,
                IconStyle.Pink  => IconResource.Logo_Pink,
                IconStyle.White => IconResource.Logo_White,
                _               => throw new NotImplementedException()
            };
    }
}
