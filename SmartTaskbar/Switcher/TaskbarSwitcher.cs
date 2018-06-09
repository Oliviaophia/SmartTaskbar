using System;
using System.Diagnostics;
using System.IO;
using static SmartTaskbar.Switcher.SafeNativeMethods;

namespace SmartTaskbar.Switcher
{
    class TaskbarSwitcher
    {
        private Process process = new Process();
        private APPBARDATA msgData = APPBARDATA.New();
        private ChildProcessTracker child = new ChildProcessTracker();
        private bool isStop = true, animation;

        public TaskbarSwitcher() => process.StartInfo.FileName = Path.Combine(Directory.GetCurrentDirectory(), Environment.Is64BitOperatingSystem ? "x64" : "x86", "TaskbarSwitcher");

        public void Hide()
        {
            Stop();
            msgData.lParam = AutoHide;
            SHAppBarMessage(SetState, ref msgData);
        }

        public void Show()
        {
            Stop();
            msgData.lParam = AlwaysOnTop;
            SHAppBarMessage(SetState, ref msgData);
        }

        public bool IsHide() => SHAppBarMessage(GetState, ref msgData) == AutoHide ? true : false;

        public void Start()
        {
            isStop = false;
            process.Start();
            child.AddProcess(process);
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
            GetSystemParameters(SPI_GETMENUANIMATION, 0, out animation, 0);
            return animation;
        }

        public bool AnimationSwitcher()
        {
            animation = !animation;
            SetSystemParameters(SPI_SETMENUANIMATION, 0, animation, 0x01 | 0x02);
            return animation;
        }
    }
}
