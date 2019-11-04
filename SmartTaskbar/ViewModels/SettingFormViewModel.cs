using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;

namespace SmartTaskbar.ViewModels
{
    public class SettingFormViewModel : ReactiveObject
    {
        private readonly CoreInvoker _coreInvoker;

        public SettingFormViewModel(CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;
            GetUserSettings();
            GetCultureResource();

            #region Command

            #endregion

            #region AutoModegroupBox

            this.WhenAnyValue(p => p.IsSettingDisable)
                .Where(p => p)
                .Subscribe(_ => coreInvoker.ModeSwitch.SetAutoMode(AutoModeType.Disable));

            this.WhenAnyValue(p => p.IsSettingForegroundMode)
                .Where(p => p)
                .Subscribe(_ => coreInvoker.ModeSwitch.SetAutoMode(AutoModeType.ForegroundMode));

            this.WhenAnyValue(p => p.IsSettingBlacklistMode)
                .Where(p => p)
                .Subscribe(_ => coreInvoker.ModeSwitch.SetAutoMode(AutoModeType.BlacklistMode));

            this.WhenAnyValue(p => p.IsSettingWhitelistMode)
                .Where(p => p)
                .Subscribe(_ => coreInvoker.ModeSwitch.SetAutoMode(AutoModeType.WhitelistMode));

            // todo    

            #endregion
        }

        #region UserConfig

        [Reactive] public IconStyle IconStyle { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetUserSettings()
        {
            IconStyle = _coreInvoker.UserSettings.IconStyle;
            switch (_coreInvoker.UserSettings.ModeType)
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

        #region AutoModeGroupBox

        [Reactive] public string SettingModeText { get; set; }

        [Reactive] public string SettingDisableText { get; set; }

        [Reactive] public string SettingForegroundModeText { get; set; }

        [Reactive] public string SettingBlacklistModeText { get; set; }

        [Reactive] public string SettingWhitelistModeText { get; set; }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetCultureResource()
        {
            SettingModeText = _coreInvoker.GetText(nameof(SettingModeText));
            SettingDisableText = _coreInvoker.GetText(nameof(SettingDisableText));
            SettingForegroundModeText = _coreInvoker.GetText(nameof(SettingForegroundModeText));
            SettingBlacklistModeText = _coreInvoker.GetText(nameof(SettingBlacklistModeText));
            SettingWhitelistModeText = _coreInvoker.GetText(nameof(SettingWhitelistModeText));
        }

        #endregion

        #region Command

        

        #endregion
    }
}