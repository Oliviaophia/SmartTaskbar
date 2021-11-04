using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class AutoHideHelper
{
    private const int AbsAutohide = 1;

    private const int AbsAlwaysontop = 2;

    private const uint AbmSetstate = 10;

    private const uint AbmGetstate = 4;

    /// <summary>
    ///     Set taskbar to Auto-Hide
    /// </summary>
    internal static void SetAutoHide()
    {
        var msg = new AppbarData();
        if (SHAppBarMessage(AbmGetstate, ref msg) != IntPtr.Zero)
            return;

        msg.lParam = AbsAutohide;

        _ = SHAppBarMessage(AbmSetstate, ref msg);
    }

    /// <summary>
    ///     Change Auto-Hide status
    /// </summary>
    internal static void ChangeAutoHide()
    {
        var msg = new AppbarData();
        msg.lParam = SHAppBarMessage(AbmGetstate, ref msg) == IntPtr.Zero ? AbsAutohide : AbsAlwaysontop;
        _ = SHAppBarMessage(AbmSetstate, ref msg);
    }

    /// <summary>
    ///     Set taskbar to Always-On-Top
    /// </summary>
    internal static void CancelAutoHide()
    {
        var msg = new AppbarData();
        if (SHAppBarMessage(AbmGetstate, ref msg) == IntPtr.Zero)
            return;

        msg.lParam = AbsAlwaysontop;

        _ = SHAppBarMessage(AbmSetstate, ref msg);
    }
}
