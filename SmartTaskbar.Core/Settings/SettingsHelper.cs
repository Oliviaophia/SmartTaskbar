using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SmartTaskbar.Core.Helpers;

namespace SmartTaskbar.Core.Settings
{
    public static class SettingsHelper
    {
        private static readonly string SettingPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                         "SmartTaskbar",
                         "SmartTaskbar.json");

        internal static async Task SaveSettingsAsync(UserSettings userSettings)
        {
            DirectoryBuilder();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using var stream = new FileStream(SettingPath, FileMode.OpenOrCreate);

            await JsonSerializer.SerializeAsync(stream, userSettings, options);
        }

        internal static async Task<UserSettings> ReadSettingsAsync()
        {
            DirectoryBuilder();
            using var fs = new FileStream(SettingPath, FileMode.OpenOrCreate);

            var settings = await JsonSerializer.DeserializeAsync<UserSettings>(fs);

            return settings.UpdateSettings();
        }

        private static void DirectoryBuilder()
            => Directory.CreateDirectory(Path.GetDirectoryName(SettingPath) ?? throw new InvalidOperationException());

        private static UserSettings UpdateSettings(this UserSettings settings)
            => new UserSettings
            {
                IconStyle = settings?.IconStyle ?? IconStyle.Auto,
                ModeType = settings?.ModeType ?? AutoModeType.AutoHideApiMode,
                ResetState = settings?.ResetState
                             ?? new TaskbarState
                             {
                                 HideTaskbarCompletely = false,
                                 IsAutoHide = !AutoHide.NotAutoHide(),
                                 IconSize = ButtonSize.GetIconSize(),
                                 TransparentMode = TransparentModeType.Disable
                             },
                ReadyState = settings?.ReadyState
                             ?? new TaskbarState
                             {
                                 HideTaskbarCompletely = false,
                                 IsAutoHide = false,
                                 IconSize = Constant.IconLarge,
                                 TransparentMode = TransparentModeType.Disable
                             },
                TargetState = settings?.TargetState
                              ?? new TaskbarState
                              {
                                  HideTaskbarCompletely = false,
                                  IsAutoHide = true,
                                  IconSize = Constant.IconLarge,
                                  TransparentMode = TransparentModeType.Disable
                              },
                BlockList = settings?.BlockList ?? new HashSet<string>(),
                Allowlist = settings?.Allowlist ?? new HashSet<string>(),
                //DisableOnTabletMode = settings?.DisableOnTabletMode ?? true,
                IconCentered = settings?.IconCentered ?? false,
                Language = settings?.Language ?? Language.Auto
            };
    }
}
