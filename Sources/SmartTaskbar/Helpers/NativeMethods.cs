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

    /// <summary>
    ///     If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style,
    ///     the return value is nonzero. Otherwise, the return value is zero.
    ///     Because the return value specifies whether the window has the WS_VISIBLE style, it may be nonzero even if the
    ///     window is totally obscured by other windows.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible([In] IntPtr hWnd);

    #endregion

    #region DwmGetWindowAttribute

    /// <summary>
    ///     Retrieves the current value of a specified Desktop Window Manager (DWM) attribute applied to a window.
    ///     If the function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="dwAttribute"></param>
    /// <param name="pvAttribute"></param>
    /// <param name="cbAttribute"></param>
    /// <returns></returns>
    [DllImport("dwmapi.dll")]
    public static extern int DwmGetWindowAttribute(IntPtr                                   hwnd,
                                                   int                                      dwAttribute,
                                                   [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute,
                                                   int                                      cbAttribute);

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

    #region SetForegroundWindow

    /// <summary>
    ///     If the window was brought to the foreground, the return value is nonzero.
    ///     If the window was not brought to the foreground, the return value is zero.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

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

    /// <summary>
    ///     If the point is contained by a display monitor, the return value is an HMONITOR handle to that display monitor.
    ///     If the point is not contained by a display monitor, the return value depends on the value of dwFlags.
    /// </summary>
    /// <param name="pt"></param>
    /// <param name="dwFlags"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "MonitorFromPoint")]
    public static extern IntPtr MonitorFromPoint(TagPoint pt, uint dwFlags);

    #endregion

    #region MonitorFromWindow

    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    #endregion

    #region FindWindow

    /// <summary>
    ///     If the function succeeds, the return value is a handle to the window that has the specified class name and window
    ///     name.
    ///     If the function fails, the return value is NULL.
    /// </summary>
    /// <param name="strClassName"></param>
    /// <param name="strWindowName"></param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindWindow([In] string? strClassName, [In] string? strWindowName);

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

    /// <summary>
    ///     This function returns a message-dependent value.
    /// </summary>
    /// <param name="dwMessage"></param>
    /// <param name="pData"></param>
    /// <returns></returns>
    [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr SHAppBarMessage(uint dwMessage, ref AppbarData pData);

    #endregion

    #region SystemParametersInfo

    /// <summary>
    ///     If the function succeeds, the return value is a nonzero value.
    ///     If the function fails, the return value is zero.
    /// </summary>
    /// <param name="uiAction"></param>
    /// <param name="uiParam"></param>
    /// <param name="pvParam"></param>
    /// <param name="fWinIni"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetSystemParameters(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

    /// <summary>
    ///     If the function succeeds, the return value is a nonzero value.
    ///     If the function fails, the return value is zero.
    /// </summary>
    /// <param name="uiAction"></param>
    /// <param name="uiParam"></param>
    /// <param name="pvParam"></param>
    /// <param name="fWinIni"></param>
    /// <returns></returns>
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

    #region GetProcessId

    /// <summary>
    ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the
    ///     process that created the window.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpdwProcessId">
    ///     A pointer to a variable that receives the process identifier. If this parameter is not NULL,
    ///     GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.
    /// </param>
    /// <returns>The return value is the identifier of the thread that created the window.</returns>
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    #endregion
}
