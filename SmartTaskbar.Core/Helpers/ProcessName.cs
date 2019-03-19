using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Settings;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ProcessName
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool InBlacklist(this IntPtr handle) => handle.InList(_ => UserSettings.blacklist.Contains(_.MainModule.ModuleName));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool InWhitelist(this IntPtr handle) => handle.InList(_ => !UserSettings.whitelist.Contains(_.MainModule.ModuleName));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool InList(this IntPtr handle, Func<Process, bool> func)
        {
            GetWindowThreadProcessId(handle, out int processId);

            using (var process = Process.GetProcessById(processId))
            {
                return func(process);
            }
        }
    }
}
