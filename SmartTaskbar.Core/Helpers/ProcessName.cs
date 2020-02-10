using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ProcessName
    {
        internal static bool InBlacklist(this IntPtr handle, HashSet<string> blacklist)
        {
            if (Variable.NameCache.TryGetValue(handle, out var name)) return blacklist.Contains(name);

            GetWindowThreadProcessId(handle, out var processId);

            using var process = Process.GetProcessById(processId);
            if (process.MainModule == null)
                return false;

            name = process.MainModule.ModuleName;
            Variable.NameCache.Add(handle, name);

            return blacklist.Contains(name);
        }

        internal static bool NotInWhitelist(this IntPtr handle, HashSet<string> whitelist)
        {
            if (Variable.NameCache.TryGetValue(handle, out var name)) return !whitelist.Contains(name);

            GetWindowThreadProcessId(handle, out var processId);

            using var process = Process.GetProcessById(processId);
            if (process.MainModule == null)
                return false;

            name = process.MainModule.ModuleName;
            Variable.NameCache.Add(handle, name);

            return !whitelist.Contains(name);
        }

        internal static Dictionary<IntPtr, string> UpdateCacheName(this Dictionary<IntPtr, string> cacheDictionary)
        {
            foreach (var key in cacheDictionary.Keys.Where(key => key.IsWindowInvisible()))
                cacheDictionary.Remove(key);

            return cacheDictionary;
        }
    }
}