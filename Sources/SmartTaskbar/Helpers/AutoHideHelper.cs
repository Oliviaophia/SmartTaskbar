using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class AutoHideHelper
{
    private const int AbsAutohide = 1;

    private const int AbsAlwaysontop = 2;
    
    private const uint AbmSetstate = 10;
    
    private const uint AbmGetstate = 4;
    
    internal static void SetAutoHide()
    {
        var msg = new AppbarData
        {
            lParam = AbsAutohide
        };
        _ = SHAppBarMessage(AbmSetstate, ref msg);
    }

    internal static void ChangeAutoHide()
    {
        var msg = new AppbarData();
        msg.lParam = SHAppBarMessage(AbmGetstate, ref msg) == IntPtr.Zero ? AbsAutohide : AbsAlwaysontop;
        _ = SHAppBarMessage(AbmSetstate, ref msg);
    }

    internal static void CancelAutoHide()
    {
        var msg = new AppbarData
        {
            lParam = AbsAlwaysontop
        };
        _ = SHAppBarMessage(AbmSetstate, ref msg);
    }
}
