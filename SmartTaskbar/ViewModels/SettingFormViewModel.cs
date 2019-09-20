using System;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Languages;

namespace SmartTaskbar.ViewModels
{
    public class SettingFormViewModel : ReactiveObject
    {
        public SettingFormViewModel(AutoModeController autoModeController)
        {
            GetUserConfig();
            GetCultureResource();

            #region Command


            #endregion

            #region AutoModegroupBox

            this.WhenAnyValue(p => p.IsSettingDisable)
                .Where(p => p)
                .Subscribe(_ => autoModeController.AutoModeSet(AutoModeType.Disable));

            this.WhenAnyValue(p => p.IsSettingForegroundMode)
                .Where(p => p)
                .Subscribe(_ => autoModeController.AutoModeSet(AutoModeType.ForegroundMode));

            this.WhenAnyValue(p => p.IsSettingBlacklistMode)
                .Where(p => p)
                .Subscribe(_ => autoModeController.AutoModeSet(AutoModeType.BlacklistMode));

            this.WhenAnyValue(p => p.IsSettingWhitelistMode)
                .Where(p => p)
                .Subscribe(_ => autoModeController.AutoModeSet(AutoModeType.WhitelistMode));

            #endregion
        }

        #region UserConfig

        [Reactive] public IconStyle IconStyle { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetUserConfig()
        {
            IconStyle = InvokeMethods.UserConfig.IconStyle;
            switch (InvokeMethods.UserConfig.ModeType)
            {
                case AutoModeType.Disable:
                    IsSettingDisable = true;
                    break;
                case AutoModeType.ForegroundMode:
                    IsSettingForegroundMode = true;
                    break;
                case AutoModeType.BlacklistMode:
                    IsSettingBlacklistMode = true;
                    break;
                case AutoModeType.WhitelistMode:
                    IsSettingWhitelistMode = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region AutoModeGroupBox

        [Reactive] public bool IsSettingDisable { get; set; }

        [Reactive] public bool IsSettingForegroundMode { get; set; }

        [Reactive] public bool IsSettingBlacklistMode { get; set; }

        [Reactive] public bool IsSettingWhitelistMode { get; set; }

        #endregion

        #endregion

        #region Language

        #region AutoModeGrupBox

        [Reactive] public string SettingModeText { get; set; }

        [Reactive] public string SettingDisableText { get; set; }

        [Reactive] public string SettingForegroundModeText { get; set; }

        [Reactive] public string SettingBlacklistModeText { get; set; }

        [Reactive] public string SettingWhitelistModeText { get; set; }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetCultureResource()
        {
            SettingModeText = CultureResource.Instance.GetText(nameof(SettingModeText));
            SettingDisableText = CultureResource.Instance.GetText(nameof(SettingDisableText));
            SettingForegroundModeText = CultureResource.Instance.GetText(nameof(SettingForegroundModeText));
            SettingBlacklistModeText = CultureResource.Instance.GetText(nameof(SettingBlacklistModeText));
            SettingWhitelistModeText = CultureResource.Instance.GetText(nameof(SettingWhitelistModeText));
        }

        #endregion

        #region Command


        #endregion
    }
}