using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.InvokeMethods;

namespace SmartTaskbar.Core.UserConfig
{
    public static class SettingsHelper
    {
        private static readonly string SettingPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SmartTaskbar",
                "SmartTaskbar.json");

        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SaveSettings()
        {
            DirectoryBuilder();
            using (FileStream fs = new FileStream(SettingPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    Serializer.Serialize(sw, Settings);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ReadSettings()
        {
            DirectoryBuilder();
            using (var fs = new FileStream(SettingPath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        GetSettings(Serializer.Deserialize<UserSettings>(jr));
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DirectoryBuilder() => Directory.CreateDirectory(Path.GetDirectoryName(SettingPath) ?? throw new InvalidOperationException());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetSettings(UserSettings settings)
        {
            if (settings is null)
            {
                Settings.DefaultState = BarState.GetDefault();
                Settings.ModeType = AutoModeType.ForegroundMode;
                Settings.Blacklist = new HashSet<string>(16);
                Settings.BlistDefaultState = BarState.GetDefault();
                Settings.BlistTargetState = BarState.GetDefault();
                Settings.Whitelist = new HashSet<string>(16);
                Settings.WlistDefaultState = BarState.GetDefault();
                Settings.WlistTargetState = BarState.GetDefault();
                Settings.TransparentType = TransparentModeType.Disabled;
                Settings.HideTaskbarCompletely = false;
                Settings.DisabledOnTabletMode = false;
            }
            else
            {
                Settings.DefaultState = settings.DefaultState ?? BarState.GetDefault();
                Settings.ModeType = settings.ModeType;
                Settings.Blacklist = settings.Blacklist ?? new HashSet<string>(16);
                Settings.BlistTargetState = settings.BlistTargetState ?? BarState.GetDefault();
                Settings.BlistDefaultState = settings.WlistDefaultState ?? BarState.GetDefault();
                Settings.Whitelist = settings.Whitelist ?? new HashSet<string>(16);
                Settings.WlistDefaultState = settings.WlistDefaultState ?? BarState.GetDefault();
                Settings.WlistTargetState = settings.WlistTargetState ?? BarState.GetDefault();
                Settings.TransparentType = settings.TransparentType;
                Settings.HideTaskbarCompletely = settings.HideTaskbarCompletely;
                Settings.Language = settings.Language;
                Settings.DisabledOnTabletMode = settings.DisabledOnTabletMode;
            }
        }
    }
}