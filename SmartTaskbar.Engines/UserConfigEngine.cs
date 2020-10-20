using System.Threading.Tasks;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;

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

        public async Task<UserConfiguration> GetUserConfigurationAsync()
            => UserConfiguration = await _userConfigService.ReadSettingsAsync();

        public Task SaveUserConfigurationAsync()
            => _userConfigService.SaveSettingsAsync(UserConfiguration);
    }
}
