using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class Animation
{
    private const uint SpiGetMenuAnimation = 0x1002;

    private const uint SpiSetMenuAnimation = 0x1003;

    private const uint UpdateAndSend = 3;

    static Animation()
        => GetTaskbarAnimation();

    internal static bool GetTaskbarAnimation()
    {
        _ = GetSystemParameters(SpiGetMenuAnimation, 0, out bool animation, 0);
        return animation;
    }

    internal static bool ChangeTaskbarAnimation()
    {
        _ = GetSystemParameters(SpiGetMenuAnimation, 0, out bool animation, 0);
        _ = SetSystemParameters(SpiSetMenuAnimation, 0, animation ? IntPtr.Zero : (IntPtr) 1, UpdateAndSend);
        return !animation;
    }
}
