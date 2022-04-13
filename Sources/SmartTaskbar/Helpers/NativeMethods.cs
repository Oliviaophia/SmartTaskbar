using System.Runtime.InteropServices;

namespace SmartTaskbar
{
    public static partial class Fun
    {
        #region PostMessage

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message.
        /// </param>
        /// <param name="wMsg">
        ///     The message to be posted.
        ///     For lists of the system-provided messages/>.
        /// </param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError. GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region GetCursorPos

        /// <summary>
        ///     Retrieves the position of the mouse cursor, in screen coordinates.
        /// </summary>
        /// <param name="lpPoint">A pointer to a POINT structure that receives the screen coordinates of the cursor.</param>
        /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out TagPoint lpPoint);

        #endregion

        #region IsWindowVisible

        /// <summary>
        ///     Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>
        ///     If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style,
        ///     the return value is true, otherwise it is false.
        ///     Because the return value specifies whether the window has the WS_VISIBLE style, it may be nonzero even if the
        ///     window is totally obscured by other windows.
        /// </returns>
        /// <remarks>
        ///     The visibility state of a window is indicated by the WS_VISIBLE style bit.
        ///     When WS_VISIBLE is set, the window is displayed and subsequent drawing into it is displayed as long as the window
        ///     has the WS_VISIBLE style.
        ///     Any drawing to a window with the WS_VISIBLE style will not be displayed if the window is obscured by other windows
        ///     or is clipped by its parent window.
        /// </remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion

        #region DwmGetWindowAttribute

        /// <summary>
        ///     Retrieves the current value of a specified attribute applied to a window.
        /// </summary>
        /// <param name="hwnd">The handle to the window from which the attribute data is retrieved.</param>
        /// <param name="dwAttribute">The attribute to retrieve, specified as a "DWMWINDOWATTRIBUTE"  value.</param>
        /// <param name="pvAttribute">
        ///     A pointer to a value that, when this function returns successfully, receives the current
        ///     value of the attribute. The type of the retrieved value depends on the value of the <paramref name="dwAttribute" />
        ///     parameter.
        /// </param>
        /// <param name="cbAttribute">
        ///     The size of the "DWMWINDOWATTRIBUTE" value being retrieved. The size is
        ///     dependent on the type of the "pvAttribute"  parameter.
        /// </param>
        /// <returns>
        ///     If this function succeeds, it returns "HResult.Code.S_OK" . Otherwise, it returns an
        ///     "HResult" error code.
        /// </returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr                                   hwnd,
                                                       int                                      dwAttribute,
                                                       [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute,
                                                       int                                      cbAttribute);

        #endregion

        #region GetForegroundWindow

        /// <summary>
        ///     Retrieves a handle to the foreground window (the window with which the user is currently
        ///     working). The system assigns a slightly higher priority to the thread that creates the
        ///     foreground window than it does to other threads.
        ///     <para>
        ///         See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505%28v=vs.85%29.aspx
        ///         for more information.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     C++ ( Type: Type: HWND )  The return value is a handle to the foreground window. The
        ///     foreground window can be NULL in certain circumstances, such as when a window is losing activation.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        #endregion

        #region SetForegroundWindow

        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard
        ///     input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly
        ///     higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns>
        ///     If the window was brought to the foreground, the return value is true.
        ///     <para>If the window was not brought to the foreground, the return value is false.</para>
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion

        #region MonitorFromPoint

        /// <summary>
        ///     If the point is contained by a display monitor, the return value is an HMONITOR handle to that display monitor.
        ///     If the point is not contained by a display monitor, the return value depends on the value of dwFlags.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromPoint(TagPoint pt, uint dwFlags);

        #endregion

        #region MonitorFromWindow

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        #endregion

        #region FindWindow

        /// <summary>
        ///     Retrieves a handle to the top-level window whose class name and window name match the specified strings. This
        ///     function does not search child windows. This function does not perform a case-sensitive search. To search child
        ///     windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        /// <param name="lpClassName">
        ///     The window class name. If lpClassName is NULL, it finds any window whose title matches the
        ///     lpWindowName parameter.
        /// </param>
        /// <param name="lpWindowName">The window name (the window's title). If this parameter is NULL, all window names match.</param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the window that has the specified
        ///     class name and window name. If the function fails, the return value is NULL.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

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
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out TagRect lpRect);

        #endregion

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

        #region Taskbar Display State

        /// <summary>
        ///     This function returns a message-dependent value.
        /// </summary>
        /// <param name="dwMessage"></param>
        /// <param name="pData"></param>
        /// <returns></returns>
        [DllImport("shell32.dll",
                   EntryPoint = "SHAppBarMessage",
                   CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SHAppBarMessage(uint dwMessage, ref AppbarData pData);

        #endregion

        #region WindowFromPoint

        /// <summary>
        ///     Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// <param name="point">The point to be checked.</param>
        /// <returns>
        ///     The return value is a handle to the window that contains the point. If no window exists at the given point,
        ///     the return value is <see cref="IntPtr.Zero" />. If the point is over a static text control, the return value is a
        ///     handle to the window under the static text control.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(TagPoint point);

        #endregion

        #region GetAncestor

        /// <summary>Retrieves the handle to the ancestor of the specified window.</summary>
        /// <param name="hWnd">
        ///     A handle to the window whose ancestor is to be retrieved. If this parameter is the desktop window,
        ///     the function returns <see cref="IntPtr.Zero" />.
        /// </param>
        /// <param name="gaFlags">The ancestor to be retrieved.</param>
        /// <returns>The handle to the ancestor window.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);

        #endregion

        #region GetClassName

        /// <summary>
        ///     Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="lpClassName">The class name string.</param>
        /// <param name="nMaxCount">
        ///     The length of the <paramref name="lpClassName" /> buffer, in characters. The buffer must be large enough to include
        ///     the terminating null character; otherwise, the class name string is truncated to <paramref name="nMaxCount" />-1
        ///     characters.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the number of characters copied to the buffer, not including the
        ///     terminating null character.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern unsafe int GetClassName(
            IntPtr hWnd,
            char*  lpClassName,
            int    nMaxCount);

        /// <summary>
        ///     Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="maxLength">The size of the string to return.</param>
        /// <returns>The class name string.</returns>
        /// <remarks>
        ///     The maximum length for lpszClassName is 256. See WNDCLASS structure documentation:
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms633576(v=vs.85).aspx.
        /// </remarks>
        public static unsafe string GetClassName(this IntPtr hWnd)
        {
            const int maxLength = 256;

            var className = stackalloc char[maxLength];
            var count = GetClassName(hWnd, className, maxLength);
            return count == 0 ? "" : new string(className, 0, count);
        }

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
    }
}
