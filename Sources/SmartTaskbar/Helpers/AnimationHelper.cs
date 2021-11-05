using static SmartTaskbar.NativeMethods;

namespace SmartTaskbar;

internal static class AnimationHelper
{
    private const uint SpiGetMenuAnimation = 0x1002;

    private const uint SpiSetMenuAnimation = 0x1003;

    private const uint UpdateAndSend = 3;

    static AnimationHelper()
        => GetTaskbarAnimation();

    /// <summary>
    ///     Get taskbar animation status
    /// </summary>
    /// <returns></returns>
    internal static bool GetTaskbarAnimation()
    {
        _ = GetSystemParameters(SpiGetMenuAnimation, 0, out var animation, 0);
        return animation;
    }

    /// <summary>
    ///     Change the animation state of the taskbar
    /// </summary>
    /// <returns></returns>
    internal static bool ChangeTaskbarAnimation()
    {
        _ = GetSystemParameters(SpiGetMenuAnimation, 0, out var animation, 0);
        _ = SetSystemParameters(SpiSetMenuAnimation, 0, animation ? IntPtr.Zero : (IntPtr) 1, UpdateAndSend);
        return !animation;
    }
}
