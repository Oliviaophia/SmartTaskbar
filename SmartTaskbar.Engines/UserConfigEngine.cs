using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.Engines
{
    public class UserConfigEngine
    {
        private static readonly List<IUserConfiguration> _viewModelList = new List<IUserConfiguration>();
        private readonly IUserConfigService _userConfigService;

        public UserConfigEngine(IUserConfigService userConfigServices)
            => _userConfigService = userConfigServices;

        public UserConfiguration UserConfiguration { get; set; }

        public async Task Initializer()
        {
            UserConfiguration = await GetUserConfigurationAsync();
            // save User Configuration at first time.
            _ = SaveUserConfigurationAsync();
        }

        private async Task<UserConfiguration> GetUserConfigurationAsync()
            => UserConfiguration = await _userConfigService.ReadSettingsAsync();

        private Task SaveUserConfigurationAsync()
            => _userConfigService.SaveSettingsAsync(UserConfiguration);

        public TViewModel InitViewModel<TViewModel>() where TViewModel : IUserConfiguration, new()
        {
            var viewModel = new TViewModel
            {
                IconStyle = UserConfiguration.IconStyle,
                AutoModeType = UserConfiguration.AutoModeType
            };

            _viewModelList.Add(viewModel);

            return viewModel;
        }

        public Task Update(Action<IUserConfiguration> action)
        {
            _viewModelList.ForEach(action);

            action(UserConfiguration);

            return SaveUserConfigurationAsync();
        }

        public bool Remove(IUserConfiguration userConfiguration)
            => _viewModelList.Remove(userConfiguration);
    }
}
