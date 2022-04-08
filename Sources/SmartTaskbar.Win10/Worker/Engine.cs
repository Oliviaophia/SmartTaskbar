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
        private static ForegroundWindowInfo _lastForegroundWindow;
        private static ForegroundWindowInfo _currentForegroundWindow;


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

            switch (taskbar.Value.ShouldMouseOverWindowShowTheTaskbar())
            {
                case TaskbarBehavior.DoNothing:
                    return;
                case TaskbarBehavior.Pending:
                    var (currentBehavior, currentInfo) =
                        taskbar.Value.ShouldForegroundWindowShowTheTaskbar();

                    switch (currentBehavior)
                    {
                        case TaskbarBehavior.DoNothing:
                            break;
                        case TaskbarBehavior.Pending:
                            var (lastBehavior, lastInfo) =
                                taskbar.Value.ShouldWindowShowTheTaskbar(_lastForegroundHandle);

                            switch (lastBehavior)
                            {
                                case TaskbarBehavior.DoNothing:
                                    break;
                                case TaskbarBehavior.Pending:
                                    switch (taskbar.Value.ShouldDesktopShowTheTaskbar())
                                    {
                                        case TaskbarBehavior.Show:
                                            taskbar.Value.ShowTaskar();
                                            break;
                                    }

                                    break;
                                case TaskbarBehavior.Show:
                                    taskbar.Value.ShowTaskar();

                                    break;
                                case TaskbarBehavior.Hide:
                                    if (lastInfo == _lastForegroundWindow) return;

                                    taskbar.Value.HideTaskbar();

                                    break;
                            }

                            _lastForegroundWindow = lastInfo;

                            break;
                        case TaskbarBehavior.Show:
                            if (_currentForegroundWindow.Handle != IntPtr.Zero)
                                _lastForegroundHandle = _currentForegroundWindow.Handle;

                            taskbar.Value.ShowTaskar();

                            break;
                        case TaskbarBehavior.Hide:
                            if (_currentForegroundWindow.Handle != IntPtr.Zero)
                                _lastForegroundHandle = _currentForegroundWindow.Handle;

                            if (currentInfo == _currentForegroundWindow) return;

                            taskbar.Value.HideTaskbar();

                            break;
                    }

                    _currentForegroundWindow = currentInfo;

                    return;
                case TaskbarBehavior.Show:
                    taskbar.Value.ShowTaskar();
                    return;
            }
        }
    }
}
