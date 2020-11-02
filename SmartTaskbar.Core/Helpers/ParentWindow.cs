using System;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ParentWindow
    {
        private const uint GaParent = 1;

        internal static IntPtr GetParentWindow(this IntPtr handle)
            => GetAncestor(handle, GaParent);
    }
}
