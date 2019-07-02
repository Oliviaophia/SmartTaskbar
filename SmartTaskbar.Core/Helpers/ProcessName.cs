using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ProcessName
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool InBlacklist(this IntPtr handle)
        {
            return handle.InList(_ => InvokeMethods.Settings.Blacklist.Contains(_));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool NotInWhitelist(this IntPtr handle)
        {
            return handle.InList(_ => !InvokeMethods.Settings.Whitelist.Contains(_));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool InList(this IntPtr handle, Func<string, bool> func)
        {
            if (Variable.nameCache.TryGetValue(handle, out var name)) return func(name);

            GetWindowThreadProcessId(handle, out var processId);

            using (var process = Process.GetProcessById(processId))
            {
                name = process.MainModule.ModuleName;
                Variable.nameCache.Add(handle, name);
                return func(name);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IDictionary<IntPtr, string> UpdateCacheName(this IDictionary<IntPtr, string> cacheDictionary)
        {
            foreach (var key in cacheDictionary.Keys)
                if (key.IsWindowInvisible())
                    cacheDictionary.Remove(key);

            return cacheDictionary;
        }
    }
}