using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SmartTaskbar;

[SuppressUnmanagedCodeSecurity]
public static class SafeNativeMethods
{
    #region PostMessage

    [DllImport("user32.dll", EntryPoint = "PostMessageW")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage([In] IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    #endregion

    #region GetParentWindow

    [DllImport("user32.dll", EntryPoint = "GetAncestor")]
    public static extern IntPtr GetAncestor([In] IntPtr hwnd, uint gaFlags);

    #endregion

    #region GetCursorPos

    [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out TagPoint lpPoint);

    #endregion

    #region GetDesktopWindow

    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    public static extern IntPtr GetDesktopWindow();

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

    #region GetForegroundWindow

    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern IntPtr GetForegroundWindow();

    #endregion

    #region GetClassName

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

    [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
    public static extern IntPtr WindowFromPoint(TagPoint point);

    [StructLayout(LayoutKind.Sequential)]
    public struct TagPoint
    {
        public int x;

        public int y;
    }

    #endregion

    #region SendNotifyMessage

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SendNotifyMessage([In] IntPtr hWnd, uint msg, UIntPtr wParam, string? lParam);

    #endregion
}
