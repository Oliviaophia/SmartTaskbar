using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTaskbar
{
    internal sealed class Engine
    {
        private static Timer _timer;
        private static readonly Stack<IntPtr> LastHideForegroundHandle = new Stack<IntPtr>();
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

        private static async void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            if (UserSettings.AutoModeType == AutoModeType.Auto)
                await Task.Run(AutoModeWorker);

            _timer.Start();
        }

        private static void AutoModeWorker()
        {
            // Make sure the taskbar has been automatically hidden, otherwise it will not work
            Fun.SetAutoHide();

            var taskbar = TaskbarHelper.InitTaskbar();

            // Some users will kill the explorer.exe under certain situation.
            // In this case, the taskbar cannot be found, just return and wait for the user to reopen the file explorer.
            if (taskbar.Handle == IntPtr.Zero)
            {
                Hooker.ReleaseHook();
                return;
            }

            Hooker.SetHook();

            switch (taskbar.CheckIfMouseOver())
            {
                case TaskbarBehavior.DoNothing:
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
                taskbar.CheckIfForegroundWindowIntersectTaskbar();

            switch (behavior)
            {
                case TaskbarBehavior.DoNothing:
                    break;
                case TaskbarBehavior.Pending:
                    if (taskbar.CheckIfDesktopShow())
                        BeforeShowBar(taskbar);
                    break;
                case TaskbarBehavior.Show:
                    BeforeShowBar(taskbar);
                    break;
                case TaskbarBehavior.Hide:
                    if (info == _currentForegroundWindow) return;

                    if (!LastHideForegroundHandle.Contains(info.Handle))
                        LastHideForegroundHandle.Push(info.Handle);

                    taskbar.HideTaskbar();
                    break;
            }

            _currentForegroundWindow = info;
        }

        private static void BeforeShowBar(in TaskbarInfo taskbar)
        {
            while (LastHideForegroundHandle.Count != 0)
            {
                if (taskbar.CheckIfWindowShouldHideTaskbar(LastHideForegroundHandle.Peek()))
                    return;

                LastHideForegroundHandle.Pop();
            }

            taskbar.ShowTaskar();
        }
    }
}
