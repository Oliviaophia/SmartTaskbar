using System;
using System.Reactive;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Languages;

namespace SmartTaskbar.Views
{
    public class AppViewModel : ReactiveObject
    {
        private static readonly Lazy<AppViewModel> LazyInstance =
            new Lazy<AppViewModel>(() => new AppViewModel(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static AppViewModel Instance => LazyInstance.Value;

        private static readonly CultureResource Resource = new CultureResource();

        private AppViewModel()
        {
            GetUserConfig();
            GetCultureResource();

            #region Command

            SetAutoMode = ReactiveCommand.Create<AutoModeType, Unit>(autoModeType =>
            {
                InvokeMethods.AutoModeSet(autoModeType);
                return Unit.Default;
            });

            #endregion

        }

        #region UserConfig

        [Reactive] public IconStyle IconStyle { get; set; }



        private void GetUserConfig()
        {
            IconStyle = InvokeMethods.UserConfig.IconStyle;
        }

        #endregion

        #region Language

        [Reactive] public string TraySettings { get; set; }

        [Reactive] public string TrayExit { get; set; }

        [Reactive] public string SettingMode { get; set; }

        [Reactive] public string SettingDisable { get; set; }

        [Reactive] public string SettingForegroundMode { get; set; }

        [Reactive] public string SettingBlacklistMode { get; set; }

        [Reactive] public string SettingWhitelistMode { get; set; }

        private void GetCultureResource()
        {
            TraySettings = Resource.GetString(nameof(TraySettings));
            TrayExit = Resource.GetString(nameof(TrayExit));
            SettingMode = Resource.GetString(nameof(SettingMode));
            SettingDisable = Resource.GetString(nameof(SettingDisable));
            SettingForegroundMode = Resource.GetString(nameof(SettingForegroundMode));
            SettingBlacklistMode = Resource.GetString(nameof(SettingBlacklistMode));
            SettingWhitelistMode = Resource.GetString(nameof(SettingWhitelistMode));
        }

        #endregion

        #region Command

        public ReactiveCommand<AutoModeType, Unit> SetAutoMode { get; }

        #endregion

    }
}