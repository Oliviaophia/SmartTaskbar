using System;
using System.Diagnostics;
using System.IO;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    class TaskbarSwitcher
    {
        private Process process = new Process();
        private bool isStop = true;
        private AutoModeType currentType = (AutoModeType)Properties.Settings.Default.TaskbarState;

        private readonly string auto_displayPath = Path.Combine(Directory.GetCurrentDirectory(), Environment.Is64BitOperatingSystem ? "x64" : "x86", "TaskbarSwitcher");

        private readonly string auto_sizePath = Path.Combine(Directory.GetCurrentDirectory(), Environment.Is64BitOperatingSystem ? "x64" : "x86", "IconSizeSwitcher");
        /// <summary>
        /// Startup process
        /// </summary>
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
        /// <summary>
        /// Start process
        /// </summary>
        /// <param name="type">AutoModeType</param>
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
        /// <summary>
        /// Shutdown process
        /// </summary>
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
        /// <summary>
        /// Restart process, if it is terminated unexpectedly
        /// </summary>
        public void Resume()
        {
            if (!isStop && process.HasExited)
            {
                process.Start();
                AddProcess(process.Handle);
            }
        }
        /// <summary>
        /// Change the display status of the Taskbar
        /// </summary>
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
        /// <summary>
        /// Show the Taskbar and reset buttons size
        /// </summary>
        public void Reset()
        {
            Show();
            SetIconSize(Properties.Settings.Default.IconSize);
        }
        ///// <summary>
        ///// Set the Taskbar buttons size
        ///// </summary>
        //public void SetSize() => SetIconSize(Properties.Settings.Default.IconSize);
        ///// <summary>
        ///// Get the taskbar animation state
        ///// </summary>
        ///// <returns>Taskbar animation state</returns>
        //public bool IsAnimationEnable() => GetTaskbarAnimation();
        ///// <summary>
        ///// Change the taskbar animation state
        ///// </summary>
        ///// <returns>Taskbar animation state</returns>
        //public bool AnimationSwitcher() => ChangeTaskbarAnimation();
        ///// <summary>
        ///// Update Taskbar Handle
        ///// </summary>
        //public void UpdateTaskbar() => UpdataTaskbarHandle();
    }
}
