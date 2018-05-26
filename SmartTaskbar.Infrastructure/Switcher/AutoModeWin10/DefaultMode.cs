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

        private int windowPID;
        private int uwpPID;

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
                    GetWindowThreadProcessId(h, out windowPID);
                    if (uwpPID == windowPID)
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
                if (uwpPID == 0)
                    if (SetuwpPID(out uwpPID))
                    {
                        maxWindow = IntPtr.Zero;
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
