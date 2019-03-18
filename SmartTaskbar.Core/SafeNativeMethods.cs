using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SmartTaskbar.Core
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        #region List of Taskbars

        internal static List<Taskbar> taskbars = new List<Taskbar>(4);

        #endregion

        #region Taskbar Display State

        [StructLayout(LayoutKind.Sequential)]
        internal struct APPBARDATA
        {

            public uint cbSize;

            public IntPtr hWnd;

            public uint uCallbackMessage;

            public uint uEdge;

            public TAGRECT rc;

            public int lParam;
        }

        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

        #endregion

        #region Taskbar Animation

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetSystemParameters(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetSystemParameters(uint uiAction, uint uiParam, out bool pvParam, uint fWinIni);

        #endregion

        #region Taskbar Buttons Size

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam);

        #endregion

        #region PostMessage

        [DllImport("user32.dll", EntryPoint = "PostMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessage(IntPtr hWnd, uint Msg,  IntPtr wParam,  IntPtr lParam);

        #endregion

        #region FindWindow

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);
        #endregion

        #region PostThreadMessage

        [DllImport("user32.dll", EntryPoint = "PostThreadMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostThreadMessage(int idThread, uint Msg,
            IntPtr wParam, IntPtr lParam);

        #endregion

        #region MonitorFromWindow

        [DllImport("user32.dll", EntryPoint = "MonitorFromWindow")]
        internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        #endregion

        #region GetWindowRect

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out TAGRECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        internal struct TAGRECT
        {
            public int left;

            public int top;

            public int right;

            public int bottom;

            public static implicit operator Rectangle(TAGRECT rect) => Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);

            public static implicit operator TAGRECT(Rectangle rectangle) => new TAGRECT
            {
                left = rectangle.Left,
                top = rectangle.Top,
                right = rectangle.Right,
                bottom = rectangle.Bottom
            };
        }
        #endregion

        #region WindowFromPoint


        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        internal static extern IntPtr WindowFromPoint(POINT point);

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;

            public int y;
        }

        #endregion

        #region GetParentWindow

        [DllImport("user32.dll", EntryPoint = "GetAncestor")]
        internal static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);


        #endregion

        #region GetCursorPos

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out POINT lpPoint);

        #endregion

        #region GetDesktopWindow

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        internal static extern IntPtr GetDesktopWindow();

        #endregion

        #region IsWindowVisible

        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion


        #region DwmGetWindowAttribute

        [DllImport("dwmapi.dll")]
        internal static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute, int cbAttribute);

        #endregion

        #region GetForegroundWindow

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        internal static extern IntPtr GetForegroundWindow();

        #endregion

        #region GetClassName

        [DllImport("user32.dll", EntryPoint = "GetClassNameW")]
        internal static extern int GetClassName(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpClassName, int nMaxCount);

        #endregion

        #region GetWindowPlacement
        [StructLayout(LayoutKind.Sequential)]
        internal struct TAGWINDOWPLACEMENT
        {

            public uint length;

            public uint flags;

            public uint showCmd;

            public Point ptMinPosition;

            public Point ptMaxPosition;

            public TAGRECT rcNormalPosition;
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowPlacement")]
        internal static extern int GetWindowPlacement(IntPtr hWnd, ref TAGWINDOWPLACEMENT lpwndpl);


        #endregion

        #region EnumWindows

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool Wndenumproc(IntPtr param0, IntPtr param1);

        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        internal static extern int EnumWindows(Wndenumproc lpEnumFunc, IntPtr lParam);
        #endregion
    }
}
