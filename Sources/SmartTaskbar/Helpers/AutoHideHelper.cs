using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal class AutoHideHelper
{
    private const int AbsAutohide = 1;
    private const int AbsAlwaysontop = 2;
    private const uint AbmSetstate = 10;
    private const uint AbmGetstate = 4;
    private static AppbarData _msgData;

    internal static void SetAutoHide()
    {
        _msgData.lParam = AbsAutohide;
        _ = SHAppBarMessage(AbmSetstate, ref _msgData);
        TaskbarHelper.HideTaskbar();
    }

    internal static void SetAutoHide(bool isAutoHide)
    {
        if (isAutoHide)
            SetAutoHide();
        else
            CancelAutoHide();
    }

    internal static void CancelAutoHide()
    {
        _msgData.lParam = AbsAlwaysontop;
        _ = SHAppBarMessage(AbmSetstate, ref _msgData);
    }

    internal static bool NotAutoHide()
        => SHAppBarMessage(AbmGetstate, ref _msgData) == IntPtr.Zero;
}
