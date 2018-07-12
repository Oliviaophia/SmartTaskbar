using System;
using System.Diagnostics;
using System.IO;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    class TaskbarSwitcher
    {
        private Process process = new Process();
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
            AddProcess(process.Handle);
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
                    Show();
                    break;
                default:
                    return;
            }
            process.Start();
            isStop = false;
            AddProcess(process.Handle);
        }

        public void Stop()
        {
            if (isStop)
                return;
            isStop = true;
            process.Kill();
            process.WaitForExit();
            currentType = AutoModeType.none;
            Reset();
        }

        public void Resume()
        {
            if (!isStop && process.HasExited)
            {
                process.Start();
                AddProcess(process.Handle);
            }
        }

        public bool IsAnimationEnable() => GetTaskbarAnimation(out animation);

        public bool AnimationSwitcher() => ChangeTaskbarAnimation(ref animation);

        public void ChangeState()
        {
            Stop();
            currentType = AutoModeType.none;
            if (IsHide())
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Reset()
        {
            Show();
            SetIconSize(Properties.Settings.Default.IconSize);
        }

        public void SetSize() => SetIconSize(Properties.Settings.Default.IconSize);
    }
}
