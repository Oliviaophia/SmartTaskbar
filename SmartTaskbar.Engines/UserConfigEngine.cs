using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.Engines
{
    public class UserConfigEngine
    {
        private static readonly List<IUserConfiguration> ViewModelList = new List<IUserConfiguration>();
        private readonly IUserConfigService _userConfigService;

        public UserConfigEngine(IUserConfigService userConfigServices)
            => _userConfigService = userConfigServices;

        public UserConfiguration UserConfiguration { get; set; }

        public async Task InitializationAsync()
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

            ViewModelList.Add(viewModel);

            return viewModel;
        }

        public Task Update(Action<IUserConfiguration> action)
        {
            ViewModelList.ForEach(action);

            action(UserConfiguration);

            return SaveUserConfigurationAsync();
        }

        public bool Remove(IUserConfiguration userConfiguration)
            => ViewModelList.Remove(userConfiguration);
    }
}
