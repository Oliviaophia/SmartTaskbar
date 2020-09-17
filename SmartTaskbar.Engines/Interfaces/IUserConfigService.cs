using SmartTaskbar.Models;
using System.Threading.Tasks;

namespace SmartTaskbar.Engines.Interfaces
{
    public interface IUserConfigService
    {
        Task<UserConfiguration> ReadSettingsAsync();
        Task SaveSettingsAsync(UserConfiguration configuration);
    }
}
