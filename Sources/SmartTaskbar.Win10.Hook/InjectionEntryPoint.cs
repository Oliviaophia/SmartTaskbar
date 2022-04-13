using System;
using System.Runtime.InteropServices;
using System.Threading;
using EasyHook;

namespace SmartTaskbar.Hook
{
    public class InjectionEntryPoint : IEntryPoint
    {
        private readonly ServerInterface _server;

        public InjectionEntryPoint(RemoteHooking.IContext context,
                                   string                 channelName)
        {
            _server = RemoteHooking.IpcConnectClient<ServerInterface>(channelName);
            _server.Ping();
        }

        public void Run(RemoteHooking.IContext context,
                        string                 channelName)
        {
            _server.Ping();
            var postMessageHook = LocalHook.Create(
                LocalHook.GetProcAddress("user32.dll", "PostMessageW"),
                new PostMessageDelegate(PostMessageHook),
                this);
            postMessageHook.ThreadACL.SetExclusiveACL(new[] {0});
            RemoteHooking.WakeUpProcess();
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    if (!Mutex.TryOpenExisting("{959d3545-aa5c-42a8-a327-6e2c079daa94}", out _))
                        break;

                    _server.Ping();
                }
            }
            finally
            {
                postMessageHook?.Dispose();
                LocalHook.Release();
            }
        }

        #region GetProcessId

        /// <summary>
        ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the
        ///     process that created the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpdwProcessId">
        ///     A pointer to a variable that receives the process identifier. If this parameter is not
        ///     NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.
        /// </param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        #endregion

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
                if (hWnd == FindWindow("Shell_TrayWnd", null)
                    && msg == 0x05D1)
                    return false;

                return PostMessage(hWnd, msg, wParam, lParam);
            }
            catch { return false; }
        }


        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string strClassName, string strWindowName);

        #endregion
    }
}
