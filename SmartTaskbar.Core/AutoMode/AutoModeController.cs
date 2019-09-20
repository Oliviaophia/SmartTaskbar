using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Core.AutoMode
{
    internal static class AutoModeController
    {
        private static IAutoMode _autoMode;

        static AutoModeController()
        {
            SetAutoMode();
        }

        public static void Run() => _autoMode?.Run();

        public static void Ready() => _autoMode?.Ready();

        public static void SetMode(AutoModeType modeType)
        {
            InvokeMethods.UserConfig.ModeType = modeType;
            SetAutoMode();
        }

        private static void SetAutoMode()
        {
            switch (InvokeMethods.UserConfig.ModeType)
            {
                case AutoModeType.Disable:
                    return;
                case AutoModeType.ForegroundMode:
                    _autoMode = new ForegroundMode();
                    return;
                case AutoModeType.BlacklistMode:
                case AutoModeType.WhitelistMode:
                    _autoMode = new AutoMode();
                    return;
                default:
                    _autoMode = null;
                    InvokeMethods.UserConfig.ModeType = AutoModeType.Disable;
                    return;
            }
        }
    }
}
