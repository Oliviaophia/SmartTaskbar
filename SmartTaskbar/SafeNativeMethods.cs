using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SmartTaskbar
{
    [SuppressUnmanagedCodeSecurity]
    class SafeNativeMethods
    {
        #region SHAppBarMessage
        public const uint GetState = 4;
        public const uint SetState = 10;

        public const int AlwaysOnTop = 0;
        public const int AutoHide = 1;

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {

            /// DWORD->unsigned int
            public uint cbSize;

            /// HWND->HWND__*
            public IntPtr hWnd;

            /// UINT->unsigned int
            public uint uCallbackMessage;

            /// UINT->unsigned int
            public uint uEdge;

            /// RECT->TagRECT
            public TagRECT rc;

            /// LPARAM->LONG_PTR->int
            public int lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TagRECT
        {

            /// LONG->int
            public int left;

            /// LONG->int
            public int top;

            /// LONG->int
            public int right;

            /// LONG->int
            public int bottom;
        }

        /// Return Type: UINT_PTR->unsigned int
        ///dwMessage: DWORD->unsigned int
        ///pData: PAPPBARDATA->_AppBarData*
        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CallingConvention = CallingConvention.StdCall)]
        public static extern uint SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

        #endregion
    }
}
