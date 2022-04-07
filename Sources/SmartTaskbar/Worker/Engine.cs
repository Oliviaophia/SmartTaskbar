using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace SmartTaskbar
{
    public sealed class Engine
    {
        private static Timer? _timer;
        private static IntPtr _lastHiddenHandle;
        private readonly UserSettings _userSettings;

        public Engine(Container container, UserSettings userSettings)
        {
            _userSettings = userSettings;
            // 125 milliseconds is a balance between user-acceptable perception and system call time.
            _timer = new Timer(container);
            _timer.Interval = 125;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        public IntPtr LastHiddenHandle
            => _lastHiddenHandle;

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (UserSettings.AutoModeType == AutoModeType.Auto)
                Task.Run(Worker);
        }

        private static void Worker()
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
                behavior = taskbar.Value.ShouldForegroundWindowShowTheTaskbar(ref _lastHiddenHandle);
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
