using System;
using System.Threading.Tasks;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.Engines
{
    public class UserConfigEngine<TViewModel> : IUserConfigEngine
        where TViewModel : UserConfiguration, new()
    {
        private readonly IUserConfigService _userConfigService;
        private UserConfiguration _userConfiguration;

        public UserConfigEngine(IUserConfigService userConfigServices)
        {
            _userConfigService = userConfigServices;

            _userConfiguration = _userConfigService.ReadSettingsAsync().GetAwaiter().GetResult();

            ViewModel = InitViewModel();
            // save User Configuration at first time.
            _ = SaveUserConfigurationAsync();
        }

        public TViewModel ViewModel { get; private set; }

        UserConfiguration IUserConfigEngine.UserConfiguration
            => _userConfiguration;

        private Task SaveUserConfigurationAsync()
            => _userConfigService.SaveSettingsAsync(ViewModel);

        public Task Update(Func<UserConfiguration, TViewModel> func)
        {
            var model = InitViewModel();

            var result = func(model);

            _userConfiguration = result;

            ViewModel = result;

            return SaveUserConfigurationAsync();
        }

        private TViewModel InitViewModel()
            => new()
            {
                AutoModeType = _userConfiguration.AutoModeType,
                IconStyle = _userConfiguration.IconStyle
            };
    }
}
