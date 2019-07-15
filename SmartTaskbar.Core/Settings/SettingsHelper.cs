using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SaveSettings()
        {
            DirectoryBuilder();
            using (var fs = new FileStream(SettingPath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    Serializer.Serialize(sw, InvokeMethods.UserConfig);
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
        private static void DirectoryBuilder()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingPath) ?? throw new InvalidOperationException());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetSettings(UserSettings settings)
        {
            if (settings is null)
            {
                InvokeMethods.UserConfig.DefaultState = BarState.GetDefault();
                InvokeMethods.UserConfig.ModeType = AutoModeType.ForegroundMode;
                InvokeMethods.UserConfig.Blacklist = new HashSet<string>(16);
                InvokeMethods.UserConfig.BlistDefaultState = BarState.GetDefault();
                InvokeMethods.UserConfig.BlistTargetState = BarState.GetDefault();
                InvokeMethods.UserConfig.Whitelist = new HashSet<string>(16);
                InvokeMethods.UserConfig.WlistDefaultState = BarState.GetDefault();
                InvokeMethods.UserConfig.WlistTargetState = BarState.GetDefault();
                InvokeMethods.UserConfig.TransparentType = TransparentModeType.Disabled;
                InvokeMethods.UserConfig.HideTaskbarCompletely = false;
                InvokeMethods.UserConfig.DisabledOnTabletMode = false;
            }
            else
            {
                InvokeMethods.UserConfig.DefaultState = settings.DefaultState ?? BarState.GetDefault();
                InvokeMethods.UserConfig.ModeType = settings.ModeType;
                InvokeMethods.UserConfig.Blacklist = settings.Blacklist ?? new HashSet<string>(16);
                InvokeMethods.UserConfig.BlistTargetState = settings.BlistTargetState ?? BarState.GetDefault();
                InvokeMethods.UserConfig.BlistDefaultState = settings.WlistDefaultState ?? BarState.GetDefault();
                InvokeMethods.UserConfig.Whitelist = settings.Whitelist ?? new HashSet<string>(16);
                InvokeMethods.UserConfig.WlistDefaultState = settings.WlistDefaultState ?? BarState.GetDefault();
                InvokeMethods.UserConfig.WlistTargetState = settings.WlistTargetState ?? BarState.GetDefault();
                InvokeMethods.UserConfig.TransparentType = settings.TransparentType;
                InvokeMethods.UserConfig.HideTaskbarCompletely = settings.HideTaskbarCompletely;
                InvokeMethods.UserConfig.Language = settings.Language;
                InvokeMethods.UserConfig.DisabledOnTabletMode = settings.DisabledOnTabletMode;
            }
        }
    }
}