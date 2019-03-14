using SmartTaskbar.Core.Helpers;
using System;
using System.Linq;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class DefaultMode : IAutoMode
    {
        private static readonly Lazy<DefaultMode> Instance = new Lazy<DefaultMode>(() => new DefaultMode());

        internal static DefaultMode Get => Instance.Value;

        private static IntPtr foregroundHandle;

        public void Reset()
        {
            taskbars.UpdateTaskbarList();
        }

        public void Run()
        {
            if (taskbars.IsMouseOverTaskbar())
            {
                return;
            }

            foregroundHandle = GetForegroundWindow();

            if (foregroundHandle.IsWindowInvisible())
            {
                return;
            }


            if (foregroundHandle.IsClassNameInvalid())
            {
                return;
            }

            if (foregroundHandle.IsMaximizeWindow())
            {
                monitor = foregroundHandle.GetMonitor();

                foreach (var taskbar in taskbars)
                {
                    taskbar.IsIntersect = taskbar.Monitor == monitor;
                }
            }
            else
            {
                GetWindowRect(foregroundHandle, out rect);
                foreach (var taskbar in taskbars)
                {
                    taskbar.IsIntersect = Intersect(rect, taskbar.Rect);
                }
            }

            var t = taskbars.FirstOrDefault(_ => !_.IsIntersect);
            if (t is null)
            {
                PostMessageHideTaskbar();
                return;
            }

            t.Monitor.PostMesssageShowTaskbar();
        }
    }
}
