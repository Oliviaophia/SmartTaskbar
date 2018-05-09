using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartTaskbar.Infrastructure.Switcher
{
    class AutoModeBase : ISwitcher
    {
        public Thread Autothread;

        public virtual void Start() => Autothread.Start();

        public virtual void Close() => Autothread.Abort();

        public AutoModeBase()
        {

        }

    }
}
