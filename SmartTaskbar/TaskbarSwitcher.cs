using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    class TaskbarSwitcher
    {
        private string switcherPath;
        private Process process;
        private APPBARDATA aPPBARDATA;
        private bool isStop;

        public TaskbarSwitcher()
        {
            string folder = "Switcher";
            if (Settings.Default.SwitcherVersion == 0)
            {
                Settings.Default.SwitcherVersion = Environment.OSVersion.Version.Major.ToString() == "10" ? 1 : 3;
                if (Environment.Is64BitOperatingSystem)
                    ++Settings.Default.SwitcherVersion;
                Settings.Default.Save();
            }
            switch (Settings.Default.SwitcherVersion)
            {
                case 1:
                    switcherPath = Path.Combine(Directory.GetCurrentDirectory(), folder, "TaskbarSwitcherWin10");
                    break;
                case 2:
                    switcherPath = Path.Combine(Directory.GetCurrentDirectory(), folder, "TaskbarSwitcherWin10_x64");
                    break;
                case 3:
                    switcherPath = Path.Combine(Directory.GetCurrentDirectory(), folder, "TaskbarSwitcher");
                    break;
                default:
                    switcherPath = Path.Combine(Directory.GetCurrentDirectory(), folder, "TaskbarSwitcher_x64");
                    break;
            }
            aPPBARDATA = new APPBARDATA();
            aPPBARDATA.cbSize = (uint)Marshal.SizeOf(aPPBARDATA);
            process = new Process();
            process.StartInfo.FileName = switcherPath;
            isStop = true;
        }

        public void Hide()
        {
            Stop();
            aPPBARDATA.lParam = AutoHide;
            SHAppBarMessage(SetState, ref aPPBARDATA);
        }

        public void Show()
        {
            Stop();
            aPPBARDATA.lParam = AlwaysOnTop;
            SHAppBarMessage(SetState, ref aPPBARDATA);
        }

        public bool IsHide() => SHAppBarMessage(GetState, ref aPPBARDATA) == AutoHide ? true : false;

        public void Start()
        {
            isStop = false;
            process.Start();
        }

        public void Resume()
        {
            if (process.HasExited)
                Start();
        }

        public void Stop()
        {
            if (isStop)
                return;
            isStop = true;
            try
            {
                process.Kill();
                process.WaitForExit();
            }
            catch { }
        }
    }
}
