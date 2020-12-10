using System;
using static SmartTaskbar.PlatformInvoke.SafeNativeMethods;

namespace SmartTaskbar.Engines.Helpers
{
    internal static class ParentWindow
    {
        private const uint GaParent = 1;


        internal static IntPtr GetParentWindow(this IntPtr handle)
            => GetAncestor(handle, GaParent);
    }
}
