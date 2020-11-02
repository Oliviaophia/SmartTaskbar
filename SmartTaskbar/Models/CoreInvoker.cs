using System;
using System.Drawing;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Languages;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Models
{
    public class CoreInvoker : IDisposable
    {
        private readonly CultureResource _cultureResource;

        public CoreInvoker()
        {
            ReloadSetting();
            ModeSwitch = new AutoModeSwitcher(this);
            _cultureResource = new CultureResource(this);
            SaveUserSettings();
        }

        public UserSettings UserSettings { get; set; }

        public AutoModeSwitcher ModeSwitch { get; }

        public void Dispose()
            => ModeSwitch?.Dispose();

        public async void ReloadSetting()
            => UserSettings = await InvokeMethods.GetUserSettings();

        public void SaveUserSettings()
            => InvokeMethods.SaveUserSettings(UserSettings);

        public string GetText(string name)
            => _cultureResource.GetText(name);

        public Icon GetIcon()
            => UserSettings.IconStyle switch
            {
                IconStyle.Black => Resources.Logo_Black,
                IconStyle.Blue  => Resources.Logo_Blue,
                IconStyle.Pink  => Resources.Logo_Pink,
                IconStyle.White => Resources.Logo_White,
                IconStyle.Auto => InvokeMethods.IsLightTheme()
                    ? Resources.Logo_Black
                    : Resources.Logo_White,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
