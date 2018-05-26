using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace SmartTaskbar.Infrastructure.Switcher
{
    [SuppressUnmanagedCodeSecurity]
    public static class SafeNativeMethods
    {
        static SafeNativeMethods()
        {
            //https://stackoverflow.com/questions/10852634/using-a-32bit-or-64bit-dll-in-c-sharp-dllimport
            LoadLibrary(Path.Combine(Path.GetDirectoryName(new Uri(typeof(SafeNativeMethods).Assembly.CodeBase).LocalPath),
                Environment.Is64BitProcess ? "x64" : "x86", "SmartTaskbar.Core.dll"));
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        #region TaskbarSwitcher

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern bool IsTaskbarAutoHide(ref APPBARDATA msgData);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern void ShowTaskbar(ref APPBARDATA msgData);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern void HideTaskbar(ref APPBARDATA msgData);

        #region AutoMode

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern bool IsCursorOverTaskbar(ref POINT cursor, ref APPBARDATA msgData);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern bool SetuwpPID(out int uwpPID);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern bool IsWindowMax(IntPtr maxWindow, ref WINDOWPLACEMENT placement);

        [DllImport("SmartTaskbar.Core.dll")]
        public static extern bool IsWindowNotMax(IntPtr hwnd, ref WINDOWPLACEMENT placement);


        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public static APPBARDATA New()
            {
                return new APPBARDATA
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA))
                };
            }
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

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {

            public static WINDOWPLACEMENT New()
            {
                return new WINDOWPLACEMENT
                {
                    length = (uint)Marshal.SizeOf(typeof(WINDOWPLACEMENT))
                };
            }

            /// UINT->unsigned int
            public uint length;

            /// UINT->unsigned int
            public uint flags;

            /// UINT->unsigned int
            public uint showCmd;

            /// POINT->tagPOINT
            public POINT ptMinPosition;

            /// POINT->tagPOINT
            public POINT ptMaxPosition;

            /// RECT->tagRECT
            public TagRECT rcNormalPosition;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {

            /// LONG->int
            public int x;

            /// LONG->int
            public int y;
        }

        #endregion

        #region EnumWindows

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        #endregion

        #region IsWindowVisible

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion

        #region GetWindowThreadProcessId

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int  lpdwProcessId);

        #endregion
    }
}
