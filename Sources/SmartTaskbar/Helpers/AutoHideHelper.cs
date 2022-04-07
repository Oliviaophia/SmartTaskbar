namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const int AbsAutoHide = 1;

        private const int AbsAlwaysOnTop = 2;

        private const uint AbmSetState = 10;

        private const uint AbmGetState = 4;

        /// <summary>
        ///     Set taskbar to Auto-Hide
        /// </summary>
        public static void SetAutoHide()
        {
            var msg = new AppbarData();
            if (SHAppBarMessage(AbmGetState, ref msg) != IntPtr.Zero)
                return;

            msg.lParam = AbsAutoHide;

            _ = SHAppBarMessage(AbmSetState, ref msg);
        }

        /// <summary>
        ///     Change Auto-Hide status
        /// </summary>
        public static void ChangeAutoHide()
        {
            var msg = new AppbarData();
            msg.lParam = SHAppBarMessage(AbmGetState, ref msg) == IntPtr.Zero ? AbsAutoHide : AbsAlwaysOnTop;
            _ = SHAppBarMessage(AbmSetState, ref msg);
        }

        /// <summary>
        ///     Set taskbar to Always-On-Top
        /// </summary>
        public static void CancelAutoHide()
        {
            var msg = new AppbarData();
            if (SHAppBarMessage(AbmGetState, ref msg) == IntPtr.Zero)
                return;

            msg.lParam = AbsAlwaysOnTop;

            _ = SHAppBarMessage(AbmSetState, ref msg);
        }
    }
}
