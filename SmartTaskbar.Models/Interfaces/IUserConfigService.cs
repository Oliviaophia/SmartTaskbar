using System.Threading.Tasks;

namespace SmartTaskbar.Models.Interfaces
{
    public interface IUserConfigService
    {
        Task<UserConfiguration> ReadSettingsAsync();
        Task SaveSettingsAsync(UserConfiguration configuration);
    }
}
