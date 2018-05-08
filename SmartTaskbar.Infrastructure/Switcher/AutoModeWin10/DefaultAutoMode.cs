using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
