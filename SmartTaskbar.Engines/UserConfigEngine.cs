using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;
using System.Threading.Tasks;

namespace SmartTaskbar.Engines
{
    public class UserConfigEngine
    {
        private readonly IUserConfigService _userConfigService;

        public UserConfiguration UserConfiguration { get; set; }

        public UserConfigEngine(IUserConfigService userconfigServices)
        {
            _userConfigService = userconfigServices;
            UserConfiguration = GetUserConfigurationAsync().GetAwaiter().GetResult();
        }

        public async Task<UserConfiguration> GetUserConfigurationAsync()
        {
            UserConfiguration = await _userConfigService.ReadSettingsAsync();

            return UserConfiguration;
        }

        public Task SaveUserConfigurationAsync() => _userConfigService.SaveSettingsAsync(UserConfiguration);
    }
}
