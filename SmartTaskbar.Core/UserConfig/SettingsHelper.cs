using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartTaskbar.Core.UserConfig
{
    public static class SettingsHelper
    {
        private static readonly string SettingPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Chanple", "SmartTaskbar", "SmartTaskbar.json");
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SaveSettings()
        {
            DirectoryBuilder();
            using (FileStream fs = new FileStream(SettingPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    Serializer.Serialize(sw, UserSettings.Get);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadSettings()
        {
            DirectoryBuilder();
            using (var fs = new FileStream(SettingPath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        UserSettings.Get.UpdateSettings(Serializer.Deserialize<UserSettings>(jr));
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DirectoryBuilder() => Directory.CreateDirectory(Path.GetDirectoryName(SettingPath) ?? throw new InvalidOperationException());
    }
}
