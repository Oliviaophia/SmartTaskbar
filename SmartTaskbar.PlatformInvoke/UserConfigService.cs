using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;

namespace SmartTaskbar.PlatformInvoke
{
    public class UserConfigService : IUserConfigService
    {
        private readonly string _userConfigPath;

        private readonly JsonSerializerOptions _options;

        public UserConfigService()
        {
            _userConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Constants.ApplicationName, "SmartTaskbar.json");

            _options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            _options.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<UserConfiguration> ReadSettingsAsync()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_userConfigPath)!);

            await using var fs = new FileStream(_userConfigPath, FileMode.OpenOrCreate);

            try { return await JsonSerializer.DeserializeAsync<UserConfiguration>(fs, _options); }
            catch
            {
                // return default setting if Deserialization process failed.
                return new UserConfiguration();
            }
        }

        public async Task SaveSettingsAsync(UserConfiguration configuration)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_userConfigPath)!);

            await using var fs = new FileStream(_userConfigPath, FileMode.OpenOrCreate);

            await JsonSerializer.SerializeAsync(fs, configuration, _options);
        }
    }
}
