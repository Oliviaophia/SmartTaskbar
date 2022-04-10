using System;

namespace SmartTaskbar
{
    public static partial class Fun
    {
        private const int AbsAutoHide = 1;

        private const int AbsAlwaysOnTop = 2;

        private const uint AbmSetState = 10;

        private const uint AbmGetState = 4;
        private static AppbarData _msg;

        /// <summary>
        ///     Set taskbar to Auto-Hide
        /// </summary>
        public static void SetAutoHide()
        {
            if (!IsNotAutoHide())
                return;

            _msg.lParam = AbsAutoHide;

            _ = SHAppBarMessage(AbmSetState, ref _msg);
        }

        public static bool IsNotAutoHide()
            => SHAppBarMessage(AbmGetState, ref _msg) == IntPtr.Zero;

        /// <summary>
        ///     Change Auto-Hide status
        /// </summary>
        public static void ChangeAutoHide()
        {
            _msg.lParam = IsNotAutoHide() ? AbsAutoHide : AbsAlwaysOnTop;
            _ = SHAppBarMessage(AbmSetState, ref _msg);
        }

        /// <summary>
        ///     Set taskbar to Always-On-Top
        /// </summary>
        public static void CancelAutoHide()
        {
            if (IsNotAutoHide())
                return;

            _msg.lParam = AbsAlwaysOnTop;

            _ = SHAppBarMessage(AbmSetState, ref _msg);
        }
    }
}
