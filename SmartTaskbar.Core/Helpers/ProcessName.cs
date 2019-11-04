using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ProcessName
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool InBlacklist(this IntPtr handle, in UserSettings userSettings)
        {
            if (Variable.NameCache.TryGetValue(handle, out var name)) return userSettings.Blacklist.Contains(name);

            GetWindowThreadProcessId(handle, out var processId);

            using var process = Process.GetProcessById(processId);
            if (process.MainModule == null)
                return false;

            name = process.MainModule.ModuleName;
            Variable.NameCache.Add(handle, name);

            return userSettings.Blacklist.Contains(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool NotInWhitelist(this IntPtr handle, in UserSettings userSettings)
        {
            if (Variable.NameCache.TryGetValue(handle, out var name)) return !userSettings.Whitelist.Contains(name);

            GetWindowThreadProcessId(handle, out var processId);

            using var process = Process.GetProcessById(processId);
            if (process.MainModule == null)
                return false;

            name = process.MainModule.ModuleName;
            Variable.NameCache.Add(handle, name);

            return !userSettings.Whitelist.Contains(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<IntPtr, string> UpdateCacheName(this Dictionary<IntPtr, string> cacheDictionary)
        {
            foreach (var key in cacheDictionary.Keys.Where(key => key.IsWindowInvisible()))
                cacheDictionary.Remove(key);

            return cacheDictionary;
        }
    }
}