using System.Runtime.InteropServices;
using System.Text;

namespace SmartTaskbar;

public static partial class Fun
{
    #region PostMessage

    /// <summary>
    ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
    ///     returns without waiting for the thread to process the message.
    ///     If the function succeeds, the return value is nonzero.
    ///     If the function fails, the return value is zero.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "PostMessageW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage([In] IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    #endregion

    #region GetParentWindow

    /// <summary>
    ///     Retrieves the handle to the ancestor of the specified window.
    ///     If hwnd parameter is the desktop window, the function returns NULL.
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="gaFlags"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "GetAncestor")]
    public static extern IntPtr GetAncestor([In] IntPtr hwnd, uint gaFlags);

    #endregion

    #region GetCursorPos

    /// <summary>
    ///     Retrieves the position of the mouse cursor, in screen coordinates.
    ///     Returns nonzero if successful or zero otherwise.
    /// </summary>
    /// <param name="lpPoint"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out TagPoint lpPoint);

    #endregion

    #region IsWindowVisible

    [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible([In] IntPtr hWnd);

    #endregion

    #region DwmGetWindowAttribute

    [DllImport("dwmapi.dll")]
    public static extern int DwmGetWindowAttribute(IntPtr                                   hwnd,
                                                   int                                      dwAttribute,
                                                   [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute,
                                                   int                                      cbAttribute);

    #endregion

    #region GetDesktopWindow

    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    public static extern IntPtr GetDesktopWindow();

    #endregion

    #region GetForegroundWindow

    /// <summary>
    ///     Retrieves a handle to the foreground window (the window with which the user is currently working).
    ///     The system assigns a slightly higher priority to the thread that creates the foreground window than it does to
    ///     other threads.
    ///     The return value is a handle to the foreground window.
    ///     The foreground window can be NULL in certain circumstances, such as when a window is losing activation.
    /// </summary>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern IntPtr GetForegroundWindow();

    #endregion

    #region GetClassName

    /// <summary>
    ///     Retrieves the name of the class to which the specified window belongs.
    ///     The length of the lpClassName buffer, in characters.
    ///     The buffer must be large enough to include the terminating null character;
    ///     otherwise, the class name string is truncated to nMaxCount-1 characters.
    ///     If the function fails, the return value is zero. 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpClassName"></param>
    /// <param name="nMaxCount"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "GetClassNameW")]
    public static extern int GetClassName([In] IntPtr hWnd,
                                          [Out] [MarshalAs(UnmanagedType.LPWStr)]
                                          StringBuilder lpClassName,
                                          int nMaxCount);

    #endregion

    #region MonitorFromPoint

    [DllImport("user32.dll", EntryPoint = "MonitorFromPoint")]
    public static extern IntPtr MonitorFromPoint(TagPoint pt, uint dwFlags);

    #endregion

    #region FindWindow

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindWindow([In] string? strClassName, [In] string? strWindowName);

    #endregion

    #region SendNotifyMessage

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SendNotifyMessage([In] IntPtr hWnd, uint msg, UIntPtr wParam, string? lParam);

    #endregion

    #region Taskbar Display State

    [StructLayout(LayoutKind.Sequential)]
    public struct AppbarData
    {
        public uint cbSize;

        public IntPtr hWnd;

        public uint uCallbackMessage;

        public uint uEdge;

        public TagRect rc;

        public int lParam;
    }

    [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr SHAppBarMessage(uint dwMessage, ref AppbarData pData);

    #endregion

    #region Taskbar Animation

    [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetSystemParameters(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

    [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetSystemParameters(uint uiAction, uint uiParam, out bool pvParam, uint fWinIni);

    #endregion

    #region GetWindowRect

    /// <summary>
    ///     Retrieves the dimensions of the bounding rectangle of the specified window.
    ///     The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
    ///     If the function succeeds, the return value is nonzero.
    ///     If the function fails, the return value is zero.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpRect"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect([In] IntPtr hWnd, out TagRect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct TagRect
    {
        public int left;

        public int top;

        public int right;

        public int bottom;
    }

    #endregion

    #region WindowFromPoint

    /// <summary>
    ///     The return value is a handle to the window that contains the point.
    ///     If no window exists at the given point, the return value is NULL.
    ///     If the point is over a static text control, the return value is a handle to the window under the static text
    ///     control.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
    public static extern IntPtr WindowFromPoint(TagPoint point);

    [StructLayout(LayoutKind.Sequential)]
    public struct TagPoint
    {
        public int x;

        public int y;
    }

    #endregion

    #region EnumWindows

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public delegate bool WndEnumProc(IntPtr param0, IntPtr param1);

    [DllImport("user32.dll", EntryPoint = "EnumWindows")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumWindows(WndEnumProc lpEnumFunc, IntPtr lParam);

    #endregion
}
