using System;
using System.Runtime.InteropServices;
using System.Threading;
using EasyHook;

namespace SmartTaskbar.Hook
{
    public class InjectionEntryPoint : IEntryPoint
    {
        private readonly ServerInterface _server;
        private LocalHook _postMessageHook;

        public InjectionEntryPoint(RemoteHooking.IContext context,
            string channelName)
        {
            _server = RemoteHooking.IpcConnectClient<ServerInterface>(channelName);
        }

        public void Run(RemoteHooking.IContext context,
            string channelName)
        {
            try
            {
                _server.Ping();
                _postMessageHook = LocalHook.Create(
                    LocalHook.GetProcAddress("user32.dll", "PostMessageW"),
                    new PostMessageDelegate(PostMessageHook), this);
                _postMessageHook.ThreadACL.SetExclusiveACL(new[] { 0 });
                RemoteHooking.WakeUpProcess();

                while (true)
                {
                    Thread.Sleep(1000);
                    _server.Ping();
                }
            }
            finally
            {
                _postMessageHook.Dispose();
                LocalHook.Release();
            }
        }

        #region PostMessage

        /// Return Type: BOOL->int
        /// hWnd: HWND->HWND__*
        /// Msg: UINT->unsigned int
        /// wParam: WPARAM->UINT_PTR->unsigned int
        /// lParam: LPARAM->LONG_PTR->int
        [DllImport("user32.dll", EntryPoint = "PostMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private delegate bool PostMessageDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private static bool PostMessageHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                return (hWnd != FindWindow("Shell_TrayWnd", null) || msg != 0x05CB) &&
                       PostMessage(hWnd, msg, wParam, lParam);
            }
            catch
            {
                return false;
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string strClassName, string strWindowName);

        #endregion
    }
}