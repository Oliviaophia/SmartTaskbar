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
            => _userConfigService = userConfigServices;

        public TViewModel ViewModel { get; private set; }

        UserConfiguration IUserConfigEngine.UserConfiguration
            => _userConfiguration;

        public async Task InitializationAsync()
        {
            ViewModel = await GetUserConfigurationAsync();
            // save User Configuration at first time.
            _ = SaveUserConfigurationAsync();
        }

        private async Task<TViewModel> GetUserConfigurationAsync()
        {
            _userConfiguration = await _userConfigService.ReadSettingsAsync();

            return ViewModel = InitViewModel();
        }

        private Task SaveUserConfigurationAsync()
            => _userConfigService.SaveSettingsAsync(ViewModel);

        public Task Update(Action<UserConfiguration> action)
        {
            var model = InitViewModel();

            action(model);

            _userConfiguration = model;

            ViewModel = model;

            return SaveUserConfigurationAsync();
        }

        private TViewModel InitViewModel()
            => new TViewModel
            {
                AutoModeType = _userConfiguration.AutoModeType,
                IconStyle = _userConfiguration.IconStyle
            };
    }
}
