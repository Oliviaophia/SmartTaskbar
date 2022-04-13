namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const uint TraySpiGetMenuAnimation = 0x1002;

        private const uint TraySpiSetMenuAnimation = 0x1003;

        private const uint TrayUpdateAndSend = 3;

        /// <summary>
        ///     Get taskbar animation status
        /// </summary>
        /// <returns></returns>
        public static bool IsEnableTaskbarAnimation()
        {
            _ = GetSystemParameters(TraySpiGetMenuAnimation, 0, out var animation, 0);
            return animation;
        }

        /// <summary>
        ///     Change the animation state of the taskbar
        /// </summary>
        /// <returns></returns>
        public static bool ChangeTaskbarAnimation()
        {
            _ = GetSystemParameters(TraySpiGetMenuAnimation, 0, out var animation, 0);
            _ = SetSystemParameters(TraySpiSetMenuAnimation,
                                    0,
                                    animation ? IntPtr.Zero : (IntPtr) 1,
                                    TrayUpdateAndSend);
            return !animation;
        }
    }
}
