using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmartTaskbar.Core.Helpers;

namespace SmartTaskbar.Core.Settings
{
    public static class SettingsHelper
    {
        private static readonly string SettingPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SmartTaskbar",
                "SmartTaskbar.json");

        private static readonly JsonSerializer Serializer = new JsonSerializer();

        internal static void SaveSettings(in UserSettings userSettings)
        {
            DirectoryBuilder();
            using var fs = new FileStream(SettingPath, FileMode.Create);
            using var sw = new StreamWriter(fs);
            Serializer.Serialize(sw, userSettings);
        }

        internal static UserSettings ReadSettings()
        {
            DirectoryBuilder();
            using var fs = new FileStream(SettingPath, FileMode.OpenOrCreate);
            using var sr = new StreamReader(fs);
            using var jr = new JsonTextReader(sr);
            return Serializer.Deserialize<UserSettings>(jr).UpdateSettings();
        }

        private static void DirectoryBuilder() =>
            Directory.CreateDirectory(Path.GetDirectoryName(SettingPath) ?? throw new InvalidOperationException());

        private static UserSettings UpdateSettings(this UserSettings settings)
            => new UserSettings
            {
                IconStyle = settings?.IconStyle ?? IconStyle.Auto,
                ModeType = settings?.ModeType ?? AutoModeType.AutoHideApiMode,
                ResetState = settings?.ResetState ?? new TaskbarState
                {
                    HideTaskbarCompletely = false,
                    IsAutoHide = !AutoHide.NotAutoHide(),
                    IconSize = ButtonSize.GetIconSize(),
                    TransparentMode = TransparentModeType.Disable
                },
                ReadyState = settings?.ReadyState ?? new TaskbarState
                {
                    HideTaskbarCompletely = false,
                    IsAutoHide = false,
                    IconSize = Constant.IconLarge,
                    TransparentMode = TransparentModeType.Disable
                },
                TargetState = settings?.TargetState ?? new TaskbarState
                {
                    HideTaskbarCompletely = false,
                    IsAutoHide = true,
                    IconSize = Constant.IconLarge,
                    TransparentMode = TransparentModeType.Disable
                },
                Blacklist = settings?.Blacklist ?? new HashSet<string>(),
                Whitelist = settings?.Whitelist ?? new HashSet<string>(),
                //DisableOnTabletMode = settings?.DisableOnTabletMode ?? true,
                IconCentered = settings?.IconCentered ?? false,
                Language = settings?.Language ?? Language.Auto
            };
    }
}