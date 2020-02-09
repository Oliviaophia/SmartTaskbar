using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class MaximizeWindow
    {
        private const uint SwMaximize = 3;

        private static TagWindowPlacement _placement = new TagWindowPlacement
            {length = (uint) Marshal.SizeOf(typeof(TagWindowPlacement))};


        internal static bool IsNotMaximizeWindow(this IntPtr handle)
        {
            if (handle.IsClassNameInvalid()) return true;

            GetWindowPlacement(handle, ref _placement);
            if (_placement.showCmd == SwMaximize) return false;

            GetWindowRect(handle, out var tagRect);
            var monitor = Screen.FromHandle(handle);
            return tagRect.top != monitor.Bounds.Top || tagRect.bottom != monitor.Bounds.Bottom ||
                   tagRect.left != monitor.Bounds.Left || tagRect.right != monitor.Bounds.Right;
        }
    }
}