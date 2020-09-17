using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;
using System;
using System.Drawing;

namespace SmartTaskbar.Tray.ViewModels
{
    public class UserConfigurationViewModel : IUserConfiguration
    {
        public IconStyle IconStyle { get; set; }

        public Icon Icon
            => IconStyle switch
            {
                // todo
                IconStyle.Auto => IconResource.Logo_Black,
                IconStyle.Black => IconResource.Logo_Black,
                IconStyle.Blue => IconResource.Logo_Blue,
                IconStyle.Pink => IconResource.Logo_Pink,
                IconStyle.White => IconResource.Logo_White,
                _ => throw new NotImplementedException()
            };
    }
}
