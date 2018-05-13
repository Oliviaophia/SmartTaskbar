using System.Threading;

namespace SmartTaskbar.Infrastructure.Switcher.AutoMode
{
    class DefaultAutoMode : AutoModeBase
    {
        public DefaultAutoMode() : base()
        {
            Autothread = new Thread(AutoMode);
        }

        private static void AutoMode()
        {
            
        }

    }
}
