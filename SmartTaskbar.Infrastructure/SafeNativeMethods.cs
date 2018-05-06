using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace SmartTaskbar.Infrastructure
{
    [SuppressUnmanagedCodeSecurity]
    public class SafeNativeMethods
    {
        static SafeNativeMethods()
        {
            //https://stackoverflow.com/questions/10852634/using-a-32bit-or-64bit-dll-in-c-sharp-dllimport
            LoadLibrary(Path.Combine(Path.GetDirectoryName(new Uri(typeof(SafeNativeMethods).Assembly.CodeBase).LocalPath),
                Environment.Is64BitProcess ? "win64" : "win32", "SmartTaskbar.Core.dll"));
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        #region TaskbarSwitcher

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern void SwitchTaskbar(ref APPBARDATA msgData);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern bool IsTaskbarAutoHide(ref APPBARDATA msgData);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern void ShowTaskbar(ref APPBARDATA msgData);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern void HideTaskbar(ref APPBARDATA msgData);

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
        #endregion


    }
}
