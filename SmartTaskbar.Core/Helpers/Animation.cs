using System;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class Animation
    {
        private const uint SpiGetMenuAnimation = 0x1002;

        private const uint SpiSetMenuAnimation = 0x1003;

        //private const uint SPIF_UPDATEINIFILE = 1;
        //private const uint SPIF_SENDWININICHANGE = 2;
        private const uint UpdateAndSend = 3;
        private static bool _animation;

        static Animation() => GetTaskbarAnimation();

        internal static bool GetTaskbarAnimation()
        {
            GetSystemParameters(SpiGetMenuAnimation, 0, out _animation, 0);
            return _animation;
        }

        internal static bool ChangeTaskbarAnimation()
        {
            _animation = !_animation;
            SetSystemParameters(SpiSetMenuAnimation, 0, _animation ? (IntPtr) 1 : IntPtr.Zero, UpdateAndSend);
            return _animation;
        }
    }
}