using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.UserConfig;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ProcessName
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool InBlacklist(this IntPtr handle) => handle.InList(_ => UserSettings.Get.Blacklist.Contains(_));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool NotInWhitelist(this IntPtr handle) =>
            handle.InList(_ => !UserSettings.Get.Whitelist.Contains(_));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool InList(this IntPtr handle, Func<string, bool> func)
        {
            if (cacheName.TryGetValue(handle, out string name)) return func(name);

            GetWindowThreadProcessId(handle, out int processId);

            using (var process = Process.GetProcessById(processId))
            {
                name = process.MainModule.ModuleName;
                cacheName.Add(handle, name);
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