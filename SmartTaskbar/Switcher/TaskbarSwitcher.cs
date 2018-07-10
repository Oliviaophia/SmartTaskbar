using System;
using System.Diagnostics;
using System.IO;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    class TaskbarSwitcher
    {
        private Process process = new Process();
        private APPBARDATA msgData = APPBARDATA.New();
        private bool isStop = true, animation;
        private AutoModeType currentType = (AutoModeType)Properties.Settings.Default.TaskbarState;

        private readonly string auto_displayPath = Path.Combine(Directory.GetCurrentDirectory(), Environment.Is64BitOperatingSystem ? "x64" : "x86", "TaskbarSwitcher");

        private readonly string auto_sizePath = Path.Combine(Directory.GetCurrentDirectory(), Environment.Is64BitOperatingSystem ? "x64" : "x86", "IconSizeSwitcher");

        public TaskbarSwitcher()
        {
            Reset();
            switch (currentType)
            {
                case AutoModeType.display:
                    process.StartInfo.FileName = auto_displayPath;
                    break;
                case AutoModeType.size:
                    process.StartInfo.FileName = auto_sizePath;
                    break;
                default:
                    return;
            }
            isStop = false;
            process.Start();
            AddProcess(process);
        }

        public void Start(AutoModeType type)
        {
            Stop();
            currentType = type;
            switch (type)
            {
                case AutoModeType.display:
                    process.StartInfo.FileName = auto_displayPath;
                    break;
                case AutoModeType.size:
                    process.StartInfo.FileName = auto_sizePath;
                    Show(ref msgData);
                    break;
                default:
                    return;
            }
            process.Start();
            isStop = false;
            AddProcess(process);
        }

        public void Stop()
        {
            if (isStop)
                return;
            isStop = true;
            if (currentType == AutoModeType.none)
                return;
            process.Kill();
            process.WaitForExit();
            currentType = AutoModeType.none;
            Reset();
        }

        public bool IsAnimationEnable() => GetTaskbarAnimation(out animation);

        public bool AnimationSwitcher() => ChangeTaskbarAnimation(ref animation);

        public void ChangeState()
        {
            currentType = AutoModeType.none;
            if (IsHide(ref msgData))
            {
                Show(ref msgData);
            }
            else
            {
                Hide(ref msgData);
            }
        }

        public void Reset()
        {
            Show(ref msgData);
            SetIconSize(Properties.Settings.Default.IconSize);
        }

        public void SetSize() => SetIconSize(Properties.Settings.Default.IconSize);
    }
}
