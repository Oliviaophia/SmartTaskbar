using System.Threading.Tasks;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;
using SmartTaskbar.Models.Interfaces;

namespace SmartTaskbar.Engines
{
    public class UserConfigEngine
    {
        private readonly IUserConfigService _userConfigService;

        public UserConfiguration UserConfiguration { get; set; }

        public UserConfigEngine(IUserConfigService userConfigServices)
            => _userConfigService = userConfigServices;

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
            var viewModel = new TViewModel {IconStyle = UserConfiguration.IconStyle};


            return viewModel;
        }
    }
}
