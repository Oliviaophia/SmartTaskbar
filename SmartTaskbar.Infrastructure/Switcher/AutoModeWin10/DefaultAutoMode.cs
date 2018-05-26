using System;
using System.Threading;
using static SmartTaskbar.Infrastructure.Switcher.SafeNativeMethods;

namespace SmartTaskbar.Infrastructure.Switcher.AutoModeWin10
{
    class DefaultMode : AutoModeBase
    {
        public DefaultMode() : base()
        {
            autothread = new Thread(AutoMode);
        }

        private static IntPtr windowPID;
        private static IntPtr uwpPID;

        private static void AutoMode()
        {
            bool tryShowBar = true;
            while (true)
            {
                while (IsCursorOverTaskbar(ref cursor, ref msgData))
                    Thread.Sleep(250);
                EnumWindows((h, l) => CallBackWin10(h, ref windowPID, ref uwpPID, ref maxWindow, ref placement), IntPtr.Zero);
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
                if (uwpPID == IntPtr.Zero)
                    if (SetuwpPID(ref uwpPID))
                    {
                        maxWindow = IntPtr.Zero;
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
