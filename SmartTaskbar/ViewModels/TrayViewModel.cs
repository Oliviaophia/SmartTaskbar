using System.Runtime.CompilerServices;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;

namespace SmartTaskbar.ViewModels
{
    public class TrayViewModel : ReactiveObject
    {
        private readonly CoreInvoker _coreInvoker;

        public TrayViewModel(CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;
            GetUserConfig();
            GetCultureResource();
        }

        #region UserConfig

        [Reactive] public IconStyle IconStyle { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetUserConfig()
        {
            IconStyle = _coreInvoker.UserSettings.IconStyle;
        }

        #endregion

        #region Language

        [Reactive] public string TraySettingsText { get; set; }

        [Reactive] public string TrayExitText { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GetCultureResource()
        {
            TraySettingsText = _coreInvoker.GetText(nameof(TraySettingsText));
            TrayExitText = _coreInvoker.GetText(nameof(TrayExitText));
        }

        #endregion
    }
}