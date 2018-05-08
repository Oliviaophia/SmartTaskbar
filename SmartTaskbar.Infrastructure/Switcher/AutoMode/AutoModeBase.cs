using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SmartTaskbar.Infrastructure.SafeNativeMethods;

namespace SmartTaskbar.Infrastructure.Switcher.AutoMode
{
    class AutoModeBase : ISwitcher
    {
        public Thread Autothread { get; set; }

        public virtual void Start() => Autothread.Start();

        public virtual void Close() => Autothread.Abort();

        public APPBARDATA msgData = new APPBARDATA();

        public WINDOWPLACEMENT placement = new WINDOWPLACEMENT();

        public Point cursor;

        public IntPtr maxWindowPID;

        public AutoModeBase()
        {
            msgData.cbSize = (uint)Marshal.SizeOf(msgData);
            placement.length = (uint)Marshal.SizeOf(placement);
        }

    }
}
