namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const int TrayAbsAutoHide = 1;

        private const int TrayAbsAlwaysOnTop = 2;

        private const uint TrayAbmSetState = 10;

        private const uint TrayAbmGetState = 4;
        private static AppbarData _msg;

        /// <summary>
        ///     Set taskbar to Auto-Hide
        /// </summary>
        public static void SetAutoHide()
        {
            if (!IsNotAutoHide())
                return;

            _msg.lParam = TrayAbsAutoHide;

            _ = SHAppBarMessage(TrayAbmSetState, ref _msg);
        }

        public static bool IsNotAutoHide()
            => SHAppBarMessage(TrayAbmGetState, ref _msg) == IntPtr.Zero;

        /// <summary>
        ///     Change Auto-Hide status
        /// </summary>
        public static void ChangeAutoHide()
        {
            _msg.lParam = IsNotAutoHide() ? TrayAbsAutoHide : TrayAbsAlwaysOnTop;
            _ = SHAppBarMessage(TrayAbmSetState, ref _msg);
        }

        /// <summary>
        ///     Set taskbar to Always-On-Top
        /// </summary>
        public static void CancelAutoHide()
        {
            if (IsNotAutoHide())
                return;

            _msg.lParam = TrayAbsAlwaysOnTop;

            _ = SHAppBarMessage(TrayAbmSetState, ref _msg);
        }
    }
}
