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
        private bool isStop, animationenable;

        public TaskbarSwitcher()
        {
            string folder = "Switcher";
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
            ChildProcessTracker.AddProcess(process);
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
            process.Kill();
            process.WaitForExit();
        }

        public bool IsAnimationEnable()
        {
            GetSystemParameters(SPI_GETMENUANIMATION, 0, out animationenable, 0);
            return animationenable;
        }

        public bool AnimationSwitcher()
        {
            animationenable = !animationenable;
            SetSystemParameters(SPI_SETMENUANIMATION, 0, animationenable, 0x01 | 0x02);
            return animationenable;
        }
    }
}
