using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    internal static class ParentWindow
    {
        private const uint GaParent = 1;

        internal static IntPtr GetParentWindow(this IntPtr handle)
            => GetAncestor(handle, GaParent);
    }
}
