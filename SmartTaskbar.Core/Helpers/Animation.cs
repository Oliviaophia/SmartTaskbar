using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class Animation
    {
        private static bool animation;
        private const uint SPI_GETMENUANIMATION = 0x1002;
        private const uint SPI_SETMENUANIMATION = 0x1003;
        //private const uint SPIF_UPDATEINIFILE = 1;
        //private const uint SPIF_SENDWININICHANGE = 2;
        private const uint UpdateAndSend = 3;

        static Animation()
        {
            GetTaskbarAnimation();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool GetTaskbarAnimation()
        {
            GetSystemParameters(SPI_GETMENUANIMATION, 0, out animation, 0);
            return animation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool ChangeTaskbarAnimation()
        {
            animation = !animation;
            SetSystemParameters(SPI_SETMENUANIMATION, 0, animation ? (IntPtr)1 : IntPtr.Zero, UpdateAndSend);
            return animation;
        }
    }
}
