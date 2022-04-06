using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTaskbar
{
    internal sealed class Engine
    {
        private static Timer _timer;

        public Engine(Container container)
        {
            // 125 milliseconds is a balance between user-acceptable perception and system call time.
            _timer = new Timer(container);
            _timer.Interval = 125;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private static ForegroundWindowInfo ForegroundWindowInfo { get; set; }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            switch (UserSettings.AutoModeType)
            {
                case AutoModeType.None:
                    break;
                case AutoModeType.Auto:
                    Task.Run(AutoModeWorker);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void AutoModeWorker()
        {
            // Make sure the taskbar has been automatically hidden, otherwise it will not work
            Fun.SetAutoHide();

            var taskbar = TaskbarHelper.InitTaskbar();

            // Some users will kill the explorer.exe under certain situation.
            // In this case, the taskbar cannot be found, just return and wait for the user to reopen the file explorer.
            if (!taskbar.HasValue)
                return;

            var behavior = taskbar.Value.ShouldMouseOverWindowShowTheTaskbar();

            if (behavior == TaskbarBehavior.Pending)
            {
                var (taskbarBehavior, foregroundWindowInfo) = taskbar.Value.ShouldForegroundWindowShowTheTaskbar();

                behavior = taskbarBehavior;

                if (behavior == TaskbarBehavior.Hide)
                {
                    if (foregroundWindowInfo != ForegroundWindowInfo)
                        taskbar.Value.HideTaskbar();

                    ForegroundWindowInfo = foregroundWindowInfo;
                    return;
                }

                ForegroundWindowInfo = foregroundWindowInfo;

                if (behavior == TaskbarBehavior.Pending)
                    behavior = taskbar.Value.ShouldDesktopShowTheTaskbar();
            }

            switch (behavior)
            {
                case TaskbarBehavior.Show:
                    taskbar.Value.ShowTaskar();
                    break;
                case TaskbarBehavior.Hide:
                    taskbar.Value.HideTaskbar();
                    break;
            }
        }
    }
}
