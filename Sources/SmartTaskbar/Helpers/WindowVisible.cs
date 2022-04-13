namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const int TrayDwmWaCloaked = 14;

        public static bool IsWindowInvisible(this IntPtr handle)
        {
            if (IsWindowVisible(handle) == false) return true;

            _ = DwmGetWindowAttribute(handle, TrayDwmWaCloaked, out var cloaked, sizeof(int));
            return cloaked;
        }
    }
}
