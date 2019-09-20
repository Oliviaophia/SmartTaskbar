using System.Runtime.CompilerServices;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Languages;

namespace SmartTaskbar.ViewModels
{
    public class TrayViewModel : ReactiveObject
    {
        private readonly AutoModeController _auoAutoModeController;

        public TrayViewModel(AutoModeController auoAutoModeController)
        {
            _auoAutoModeController = auoAutoModeController;
            GetUserConfig();
            GetCultureResource();
        }

        #region UserConfig

        [Reactive] public IconStyle IconStyle { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetUserConfig()
        {
            IconStyle = InvokeMethods.UserConfig.IconStyle;
        }

        #endregion

        #region Language

        [Reactive] public string TraySettingsText { get; set; }

        [Reactive] public string TrayExitText { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetCultureResource()
        {
            TraySettingsText = CultureResource.Instance.GetText(nameof(TraySettingsText));
            TrayExitText = CultureResource.Instance.GetText(nameof(TrayExitText)); }

        #endregion
    }
}