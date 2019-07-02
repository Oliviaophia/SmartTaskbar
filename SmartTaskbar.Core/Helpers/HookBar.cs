using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using EasyHook;
using SmartTaskbar.Hook;

namespace SmartTaskbar.Core.Helpers
{
    internal static class HookBar
    {
        private static int _targetPid;
        private static string _channelName;

        private static readonly string InjectionLibrary =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(), "SmartTaskbar.Hook.dll");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetHook()
        {
            var explorer = Process.GetProcessesByName("explorer").FirstOrDefault();
            if (explorer is null) return;

            if (explorer.Id == _targetPid) return;

            _targetPid = explorer.Id;
            RemoteHooking.IpcCreateServer<ServerInterface>(ref _channelName, WellKnownObjectMode.Singleton);

            RemoteHooking.Inject(
                _targetPid,
                InjectionLibrary,
                InjectionLibrary,
                _channelName
            );
        }
    }
}