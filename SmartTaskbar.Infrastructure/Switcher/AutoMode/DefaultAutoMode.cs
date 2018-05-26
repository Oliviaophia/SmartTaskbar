using System;
using System.Threading;
using static SmartTaskbar.Infrastructure.Switcher.SafeNativeMethods;

namespace SmartTaskbar.Infrastructure.Switcher.AutoMode
{
    class DefaultAutoMode : AutoModeBase
    {
        public DefaultAutoMode() : base()
        {
            autothread = new Thread(AutoMode);
        }

        private static void AutoMode()
        {
            bool tryShowBar = true;

            while (true)
            {
                while (IsCursorOverTaskbar(ref cursor, ref msgData))
                    Thread.Sleep(250);
                EnumWindows((h, l) => CallBack(h, ref maxWindow, ref placement), IntPtr.Zero);
                if (maxWindow == IntPtr.Zero)
                {
                    if (tryShowBar == false)
                    {
                        Thread.Sleep(375);
                        continue;
                    }
                    tryShowBar = false;
                    ShowTaskbar(ref msgData);
                    Thread.Sleep(500);
                    continue;
                }
                HideTaskbar(ref msgData);
                WhileMax(maxWindow, ref placement);
                tryShowBar = true;
                maxWindow = IntPtr.Zero;
            }
        }
    }
}
