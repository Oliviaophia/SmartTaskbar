using System;
using System.Threading;
using static SmartTaskbar.Infrastructure.Switcher.SafeNativeMethods;

namespace SmartTaskbar.Infrastructure.Switcher.AutoMode
{
    class DefaultMode : AutoModeBase
    {
        public DefaultMode() : base()
        {
            autothread = new Thread(AutoMode);
        }

        private void AutoMode()
        {
            bool tryShowBar = true;
            while (true)
            {
                while (IsCursorOverTaskbar(ref cursor, ref msgData))
                    Thread.Sleep(250);
                EnumWindows((h, l) =>
                {
                    if (!IsWindowVisible(h))
                        return true;
                    if (IsWindowNotMax(h, ref placement))
                        return true;
                    maxWindow = h;
                    return false;
                }, IntPtr.Zero);
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
                while(IsWindowMax(maxWindow, ref placement));
                tryShowBar = true;
                maxWindow = IntPtr.Zero;
            }
        }
    }
}
