using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTaskbar
{
    internal sealed class Engine
    {
        private static Timer _timer;
        private static IntPtr _lastForegroundHandle;
        private static ForegroundWindowInfo _lastForegroundWindow = ForegroundWindowInfo.Empty;
        private static ForegroundWindowInfo _currentForegroundWindow = ForegroundWindowInfo.Empty;


        public Engine(Container container)
        {
            // 125 milliseconds is a balance between user-acceptable perception and system call time.
            _timer = new Timer(container)
            {
                Interval = 125
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (UserSettings.AutoModeType == AutoModeType.Auto)
                Task.Run(AutoModeWorker);
        }

        private static void AutoModeWorker()
        {
            // Make sure the taskbar has been automatically hidden, otherwise it will not work
            Fun.SetAutoHide();

            var taskbar = TaskbarHelper.InitTaskbar();

            // Some users will kill the explorer.exe under certain situation.
            // In this case, the taskbar cannot be found, just return and wait for the user to reopen the file explorer.
            if (taskbar.Handle == IntPtr.Zero)
                return;

            switch (taskbar.CheckIfMouseOver())
            {
                case TaskbarBehavior.DoNothing:
                    taskbar.ShowTaskar();
                    return;
                case TaskbarBehavior.Pending:
                    CheckCurrentWindow(taskbar);

                    return;
                case TaskbarBehavior.Show:
                    taskbar.ShowTaskar();
                    return;
            }
        }

        private static void CheckCurrentWindow(in TaskbarInfo taskbar)
        {
            var (behavior, info) =
                taskbar.CheckIfWindowIntersectTaskbar(Fun.GetForegroundWindow());

            switch (behavior)
            {
                case TaskbarBehavior.DoNothing:
                    break;
                case TaskbarBehavior.Pending:
                    CheckLastWindow(taskbar);
                    break;
                case TaskbarBehavior.Show:
                    taskbar.ShowTaskar();
                    break;
                case TaskbarBehavior.Hide:
                    if (info == _currentForegroundWindow) return;

                    taskbar.HideTaskbar();
                    break;
            }

            // Determine whether the last foreground window exists, and if so, save its handle.
            if (info.Handle != IntPtr.Zero)
                _lastForegroundHandle = info.Handle;

            _currentForegroundWindow = info;
        }

        private static void CheckLastWindow(in TaskbarInfo taskbar)
        {
            var (behavior, info) =
                taskbar.CheckIfWindowIntersectTaskbar(_lastForegroundHandle);

            switch (behavior)
            {
                case TaskbarBehavior.DoNothing:
                    break;
                case TaskbarBehavior.Pending:
                    if (taskbar.CheckIfDesktopShow() == TaskbarBehavior.Show)
                        taskbar.ShowTaskar();
                    break;
                case TaskbarBehavior.Show:
                    taskbar.ShowTaskar();
                    break;
                case TaskbarBehavior.Hide:
                    if (info == _lastForegroundWindow) return;

                    taskbar.HideTaskbar();

                    break;
            }

            _lastForegroundWindow = info;
        }
    }
}
