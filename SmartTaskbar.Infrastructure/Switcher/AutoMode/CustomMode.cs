using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartTaskbar.Infrastructure.Switcher.AutoMode
{
    class CustomMode : AutoModeBase
    {
        public CustomMode() : base()
        {
            autothread = new Thread(AutoMode);
        }

        private void AutoMode()
        {
            throw new NotImplementedException();
        }
    }
}
