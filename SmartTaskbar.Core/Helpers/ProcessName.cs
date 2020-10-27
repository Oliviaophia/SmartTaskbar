using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ProcessName
    {
        internal static bool InBlockList(this IntPtr handle, HashSet<string> blockList)
        {
            if (Variable.NameCache.TryGetValue(handle, out var name)) return blockList.Contains(name);

            GetWindowThreadProcessId(handle, out var processId);

            using var process = Process.GetProcessById(processId);
            if (process.MainModule == null)
                return false;

            name = process.MainModule.ModuleName;
            Variable.NameCache.Add(handle, name);

            return blockList.Contains(name);
        }

        internal static bool NotInAllowlist(this IntPtr handle, HashSet<string> allowlist)
        {
            if (Variable.NameCache.TryGetValue(handle, out var name)) return !allowlist.Contains(name);

            GetWindowThreadProcessId(handle, out var processId);

            using var process = Process.GetProcessById(processId);
            if (process.MainModule == null)
                return false;

            name = process.MainModule.ModuleName;
            Variable.NameCache.Add(handle, name);

            return !allowlist.Contains(name);
        }

        internal static Dictionary<IntPtr, string> UpdateCacheName(this Dictionary<IntPtr, string> cacheDictionary)
        {
            foreach (var key in cacheDictionary.Keys.Where(key => key.IsWindowInvisible()))
                cacheDictionary.Remove(key);

            return cacheDictionary;
        }
    }
}