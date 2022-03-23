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

        private static void Timer_Tick(object sender, EventArgs e)
        {
            switch (UserSettings.AutoModeType)
            {
                case AutoModeType.None:
                    break;
                case AutoModeType.Display:
                    Task.Run(DisplayModeWorker);
                    break;
                case AutoModeType.Size:
                    Task.Run(SizeModeWorker);
                    break;
                case AutoModeType.Auto:
                    Task.Run(AutoModeWorker);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void DisplayModeWorker()
        {
            var taskbar = TaskbarHelper.InitTaskbar();

            // Some users will kill the explorer.exe under certain situation.
            // In this case, the taskbar cannot be found, just return and wait for the user to reopen the file explorer.
            if (!taskbar.HasValue)
                return;

            var behavior = taskbar.Value.ShouldMouseOverWindowShowTheTaskbar();

            if (behavior == TaskbarBehavior.Pending) behavior = taskbar.Value.ShouldMaximizedWindowHideTheTaskbar();

            switch (behavior)
            {
                case TaskbarBehavior.Show:
                    Fun.CancelAutoHide();
                    break;
                case TaskbarBehavior.Hide:
                    Fun.SetAutoHide();
                    break;
            }
        }

        private static void SizeModeWorker()
        {
            Fun.CancelAutoHide();

            var taskbar = TaskbarHelper.InitTaskbar();

            // Some users will kill the explorer.exe under certain situation.
            // In this case, the taskbar cannot be found, just return and wait for the user to reopen the file explorer.
            if (!taskbar.HasValue)
                return;

            var behavior = taskbar.Value.ShouldMouseOverWindowShowTheTaskbar();

            if (behavior == TaskbarBehavior.Pending) behavior = taskbar.Value.ShouldMaximizedWindowHideTheTaskbar();


            switch (behavior)
            {
                case TaskbarBehavior.Show:
                    Fun.SetBigIcon();
                    break;
                case TaskbarBehavior.Hide:
                    Fun.SetSmallIcon();
                    break;
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
                behavior = taskbar.Value.ShouldForegroundWindowShowTheTaskbar();
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