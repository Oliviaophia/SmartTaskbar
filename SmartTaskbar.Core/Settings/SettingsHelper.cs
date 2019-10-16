using System;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace SmartTaskbar.Core.Settings
{
    public static class SettingsHelper
    {
        private static readonly string SettingPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SmartTaskbar",
                "SmartTaskbar.json");

        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SaveSettings(in UserSettings userSettings)
        {
            DirectoryBuilder();
            using (var fs = new FileStream(SettingPath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    Serializer.Serialize(sw, userSettings);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UserSettings ReadSettings()
        {
            DirectoryBuilder();
            using (var fs = new FileStream(SettingPath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        return Serializer.Deserialize<UserSettings>(jr);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DirectoryBuilder()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingPath) ?? throw new InvalidOperationException());
        }
    }
}