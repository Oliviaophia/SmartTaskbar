using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class MaximizeWindow
    {
        private static TAGWINDOWPLACEMENT placement = new TAGWINDOWPLACEMENT { length = (uint)Marshal.SizeOf(typeof(TAGWINDOWPLACEMENT)) };

        private static Screen monitor;
        private static TAGRECT tagRect;
        private const uint SW_MAXIMIZE = 3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsMaximizeWindow(this IntPtr handle)
        {
            GetWindowPlacement(handle, ref placement);
            if (placement.showCmd == SW_MAXIMIZE)
            {
                return true;
            }

            GetWindowRect(handle, out tagRect);
            monitor = Screen.FromHandle(handle);
            return tagRect.top == monitor.Bounds.Top &&
                   tagRect.bottom == monitor.Bounds.Bottom &&
                   tagRect.left == monitor.Bounds.Left &&
                   tagRect.right == monitor.Bounds.Right;
        }
    }
}
