using System;

namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const uint SpiGetMenuAnimation = 0x1002;

        private const uint SpiSetMenuAnimation = 0x1003;

        private const uint UpdateAndSend = 3;

        /// <summary>
        ///     Get taskbar animation status
        /// </summary>
        /// <returns></returns>
        public static bool IsEnableTaskbarAnimation()
        {
            _ = GetSystemParameters(SpiGetMenuAnimation, 0, out var animation, 0);
            return animation;
        }

        /// <summary>
        ///     Change the animation state of the taskbar
        /// </summary>
        /// <returns></returns>
        public static bool ChangeTaskbarAnimation()
        {
            _ = GetSystemParameters(SpiGetMenuAnimation, 0, out var animation, 0);
            _ = SetSystemParameters(SpiSetMenuAnimation, 0, animation ? IntPtr.Zero : (IntPtr) 1, UpdateAndSend);
            return !animation;
        }
    }
}
