using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;
using EasyHook;
using SmartTaskbar.Hook;

namespace SmartTaskbar
{
    public static class HookHelper
    {
        private static int _targetPid;
        private static string _channelName;

        private static readonly string InjectionLibrary =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new InvalidOperationException(),
                "SmartTaskbar.Hook.dll");

        private static IpcServerChannel _channel;

        public static void ReleaseHook()
        {
            if (_channel is null) return;

            _channel.StopListening(null);

            _channel = null;
            _channelName = null;
        }

        public static void SetHook()
        {
            if (_channel != null)
                return;

            _targetPid = TaskbarHelper.GetExplorerId();

            if (_targetPid == 0)
                return;

            _channel = RemoteHooking.IpcCreateServer<ServerInterface>(ref _channelName, WellKnownObjectMode.Singleton);

            RemoteHooking.Inject(
                _targetPid,
                InjectionLibrary,
                InjectionLibrary,
                _channelName);
        }
    }
}
