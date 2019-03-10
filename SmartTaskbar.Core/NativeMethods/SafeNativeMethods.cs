using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SmartTaskbar.Core
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        #region Value

        internal static TagWindowplacement placement = new TagWindowplacement { length = (uint)Marshal.SizeOf(typeof(TagWindowplacement)) };

        internal static List<Taskbar> _taskbars = new List<Taskbar>();

        internal static IntPtr _foreWindow = IntPtr.Zero;

        internal static StringBuilder sb = new StringBuilder(255);

        internal static bool cloakedval = true;

        internal static TagRect lpRect;

        internal static bool animation;

        #endregion


        #region Taskbar Display State

        internal static Appbardata msgData = new Appbardata { cbSize = (uint)Marshal.SizeOf(typeof(Appbardata)) };

        [StructLayout(LayoutKind.Sequential)]
        internal struct Appbardata
        {

            /// DWORD->unsigned int
            public uint cbSize;

            /// HWND->HWND__*
            public IntPtr hWnd;

            /// UINT->unsigned int
            public uint uCallbackMessage;

            /// UINT->unsigned int
            public uint uEdge;

            /// RECT->Rectangle
            public Rectangle rc;

            /// LPARAM->LONG_PTR->int
            public int lParam;
        }

        /// Return Type: UINT_PTR->unsigned int
        ///dwMessage: DWORD->unsigned int
        ///pData: PAPPBARDATA->_AppBarData*
        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SHAppBarMessage(uint dwMessage, ref Appbardata pData);
        /// <summary>
        /// Set Auto-Hide Mode
        /// </summary>
        public static void Hide(this IntPtr handle)
        {
            msgData.lParam = 1;
            msgData.hWnd = handle;
            SHAppBarMessage(10, ref msgData);
            //if (IsWin10)
            //{
            //    //see https://github.com/ChanpleCai/SmartTaskbar/issues/27
            //    PostMessageW(FindWindow("Shell_TrayWnd", null), 0x05CB, (IntPtr)0, (IntPtr)0);
            //}
        }
        /// <summary>
        /// Set AlwaysOnTop Mode
        /// </summary>
        public static void Show()
        {
            msgData.lParam = 0;
            SHAppBarMessage(10, ref msgData);
        }
        /// <summary>
        /// Indicate if the Taskbar is Auto-Hide
        /// </summary>
        /// <returns>Return true when Auto-Hide</returns>
        public static bool IsHide() => SHAppBarMessage(4, ref msgData) == (IntPtr)1;

        #endregion

        #region Taskbar Animation

        /// Return Type: BOOL->bool
        ///uiAction: UINT->uint
        ///uiParam: UINT->uint
        ///pvParam: PVOID->bool
        ///fWinIni: UINT->uint
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetSystemParameters(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        /// Return Type: BOOL->bool
        ///uiAction: UINT->uint
        ///uiParam: UINT->uint
        ///pvParam: PVOID->out bool
        ///fWinIni: UINT->uint
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetSystemParameters(uint uiAction, uint uiParam, out bool pvParam, uint fWinIni);

        /// <summary>
        /// Get the taskbar animation state
        /// </summary>
        /// <returns>Taskbar animation state</returns>
        public static bool GetTaskbarAnimation()
        {
            GetSystemParameters(0x1002, 0, out animation, 0);
            return animation;
        }
        /// <summary>
        /// Change the taskbar animation state
        /// </summary>
        /// <returns>Taskbar animation state</returns>
        public static bool ChangeTaskbarAnimation()
        {
            animation = !animation;
            SetSystemParameters(0x1003, 0, animation ? (IntPtr)1 : IntPtr.Zero, 0x01 | 0x02);
            return animation;
        }
        #endregion

        #region Taskbar Buttons Size

        /// Return Type: BOOL->bool
        ///hWnd: HWND->IntPtr
        ///Msg: UINT->uint
        ///wParam: WPARAM->UINT_PTR->UIntPtr
        ///lParam: LPARAM->LONG_PTR->string
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam);

        private static readonly RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);

        public const int SmallIcon = 1;
        public const int BigIcon = 0;
        /// <summary>
        /// Modify the Taskbar buttons size
        /// </summary>
        /// <param name="size">1 = small，0 = big</param>
        public static void SetIconSize(int size)
        {
            Key.SetValue("TaskbarSmallIcons", size);
            SendNotifyMessage((IntPtr)0xffff, 0x001a, UIntPtr.Zero, "TraySettings");
        }

        /// <summary>
        /// Get the Taskbar buttons size
        /// </summary>
        /// <returns>1 = small，0 = big</returns>
        public static int GetIconSize() => (int)Key.GetValue("TaskbarSmallIcons", BigIcon);

        ///// <summary>
        ///// Change the Taskbar buttons size
        ///// </summary>
        //public static void ChangeIconSize() => SetIconSize(IsMax ? SmallIcon : BigIcon);

        #endregion

        #region PostMessage

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        ///Msg: UINT->unsigned int
        ///wParam: WPARAM->UINT_PTR->unsigned int
        ///lParam: LPARAM->LONG_PTR->int
        [DllImport("user32.dll", EntryPoint = "PostMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg,  IntPtr wParam,  IntPtr lParam);

        internal static void PostMessageHideTaskbar() =>
            PostMessage(FindWindow("Shell_TrayWnd", null), 0x05CB, IntPtr.Zero, IntPtr.Zero);

        internal static void PostMesssageShowTaskbar(this IntPtr handle) =>
            PostMessage(FindWindow("Shell_TrayWnd", null), 0x05CB, (IntPtr) 1, handle);


        #endregion

        #region FindWindow

        /// Return Type: HWND->HWND__*
        ///lpClassName: LPCWSTR->WCHAR*
        ///lpWindowName: LPCWSTR->WCHAR*
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string strClassName, string strWindowName);

        /// Return Type: HWND->HWND__*
        ///hWndParent: HWND->HWND__*
        ///hWndChildAfter: HWND->HWND__*
        ///lpszClass: LPCWSTR->WCHAR*
        ///lpszWindow: LPCWSTR->WCHAR*
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);
        #endregion

        #region PostThreadMessage

        /// Return Type: BOOL->int
        ///idThread: DWORD->unsigned int
        ///Msg: UINT->unsigned int
        ///wParam: WPARAM->UINT_PTR->unsigned int
        ///lParam: LPARAM->LONG_PTR->int
        [DllImport("user32.dll", EntryPoint = "PostThreadMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostThreadMessageW(int idThread, uint Msg,
            IntPtr wParam, IntPtr lParam);

        #endregion

        #region MonitorFromWindow

        /// Return Type: HMONITOR->HMONITOR__*
        ///hwnd: HWND->HWND__*
        ///dwFlags: DWORD->unsigned int
        [DllImport("user32.dll", EntryPoint = "MonitorFromWindow")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);
        private const int MonitorDefaulttonearest = 2;

        internal static IntPtr GetMonitor(this IntPtr handle) => MonitorFromWindow(handle, MonitorDefaulttonearest);

        #endregion

        #region GetWindowRect

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        ///lpRect: LPRECT->Rectangle*
        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out TagRect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        internal struct TagRect
        {

            /// LONG->int
            public int left;

            /// LONG->int
            public int top;

            /// LONG->int
            public int right;

            /// LONG->int
            public int bottom;

            public static implicit operator Rectangle(TagRect rect) => Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);

            public static implicit operator TagRect(Rectangle rectangle) => new TagRect
            {
                left = rectangle.Left,
                top = rectangle.Top,
                right = rectangle.Right,
                bottom = rectangle.Bottom
            };
        }
        #endregion

        #region WindowFromPoint

        /// Return Type: HWND->HWND__*
        ///Point: POINT->tagPOINT
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        internal static extern IntPtr WindowFromPoint(TagPoint point);

        [StructLayout(LayoutKind.Sequential)]
        internal struct TagPoint
        {

            /// LONG->int
            public int x;

            /// LONG->int
            public int y;

            public static implicit operator TagPoint(Point point) => new TagPoint
            {
                x = point.X,
                y = point.Y
            };

            public static implicit operator Point(TagPoint point) => new Point(point.x, point.y);
        }

        #endregion


        #region IsWindowVisible

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion


        #region DwmGetWindowAttribute

        [DllImport("dwmapi.dll")]
        internal static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute, int cbAttribute);

        #endregion

        #region GetForegroundWindow

        /// Return Type: HWND->HWND__*
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        internal static extern IntPtr GetForegroundWindow();

        #endregion

        #region GetClassName

        /// Return Type: int
        ///hWnd: HWND->HWND__*
        ///lpClassName: LPWSTR->WCHAR*
        ///nMaxCount: int
        [DllImport("user32.dll", EntryPoint = "GetClassNameW")]
        internal static extern int GetClassName(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpClassName, int nMaxCount);

        #endregion

        #region GetWindowPlacement
        [StructLayout(LayoutKind.Sequential)]
        internal struct TagWindowplacement
        {

            /// UINT->unsigned int
            public uint length;

            /// UINT->unsigned int
            public uint flags;

            /// UINT->unsigned int
            public uint showCmd;

            /// POINT->Point
            public Point ptMinPosition;

            /// POINT->Point
            public Point ptMaxPosition;

            /// RECT->TagRECT
            public TagRect rcNormalPosition;
        }

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        ///lpwndpl: WINDOWPLACEMENT*
        [DllImport("user32.dll", EntryPoint = "GetWindowPlacement")]
        private static extern int GetWindowPlacement(IntPtr hWnd, ref TagWindowplacement lpwndpl);

        internal static bool IsMaxWindow(this IntPtr handle)
        {
            GetWindowPlacement(handle, ref placement);
            if (placement.showCmd == 3)
            {
                return true;
            }

            GetWindowRect(handle, out lpRect);
            var monitor = Screen.FromHandle(handle);

            return lpRect.top == monitor.Bounds.Top &&
                   lpRect.bottom == monitor.Bounds.Bottom &&
                   lpRect.left == monitor.Bounds.Left &&
                   lpRect.right == monitor.Bounds.Right;
        }


        #endregion

        #region EnumWindows

        /// Return Type: BOOL->int
        ///param0: HWND->HWND__*
        ///param1: LPARAM->LONG_PTR->int
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool Wndenumproc(IntPtr param0, IntPtr param1);

        /// Return Type: BOOL->int
        ///lpEnumFunc: WNDENUMPROC
        ///lParam: LPARAM->LONG_PTR->int
        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        internal static extern int EnumWindows(Wndenumproc lpEnumFunc, IntPtr lParam);
        #endregion
    }
}
