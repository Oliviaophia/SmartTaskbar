using System;
using System.Threading;
using static SmartTaskbar.Infrastructure.Switcher.SafeNativeMethods;

namespace SmartTaskbar.Infrastructure.Switcher
{
    class AutoModeBase : ISwitcher
    {
        public virtual void Start() => autothread.Start();

        public virtual void Close() => autothread.Abort();

        internal Thread autothread;

        internal APPBARDATA msgData = APPBARDATA.New();

        internal WINDOWPLACEMENT placement = WINDOWPLACEMENT.New();

        internal POINT cursor = new POINT();

        internal IntPtr maxWindow;

        public AutoModeBase()
        {

        }

    }
}
