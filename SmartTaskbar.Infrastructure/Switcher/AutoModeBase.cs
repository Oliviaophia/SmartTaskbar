using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SmartTaskbar.Infrastructure.Switcher.SafeNativeMethods;

namespace SmartTaskbar.Infrastructure.Switcher
{
    class AutoModeBase : ISwitcher
    {
        internal Thread autothread;

        public virtual void Start() => autothread.Start();

        public virtual void Close() => autothread.Abort();

        internal static APPBARDATA msgData = APPBARDATA.New();

        internal static WINDOWPLACEMENT placement = WINDOWPLACEMENT.New();

        internal static POINT cursor = new POINT();

        internal static IntPtr maxWindow;

        public AutoModeBase()
        {

        }

    }
}
