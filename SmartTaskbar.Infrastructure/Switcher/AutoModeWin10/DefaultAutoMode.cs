using System;
using System.Threading;

namespace SmartTaskbar.Infrastructure.Switcher.AutoModeWin10
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
