using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SmartTaskbar.SafeNativeMethods;
namespace SmartTaskbar
{
    class TaskbarSwitcher
    {
        private string switcherPath;
        private Process process;
        private APPBARDATA aPPBARDATA;

        public TaskbarSwitcher()
        {
            string folder = "Switcher";
            if (Settings.Default.SwitcherVersion == 0)
            {
                Settings.Default.SwitcherVersion = Environment.OSVersion.Version.Major.ToString() == "10" ? 1 : 3;
                if (Environment.Is64BitOperatingSystem)
                    Settings.Default.SwitcherVersion++;
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
            if (Settings.Default.TaskbarState == 0)
                Start();
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

        public bool IsHide() => SHAppBarMessage(GetState, ref aPPBARDATA) == 1 ? true : false;

        public void Start() => process.Start();

        public void Resume()
        {
            if (process.HasExited)
                Start();
        }

        public void Stop()
        {
            try
            {
                process.Kill();
                process.WaitForExit();
            }
            catch { }
        }

        ~TaskbarSwitcher()
        {
            Stop();
        }
    }
}
