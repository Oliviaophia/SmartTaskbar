using System.Linq;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class ForegroundMode : IAutoMode
    {
        private static bool _sendMessage;

        public ForegroundMode() { Reset(); }

        public void Ready()
        {
            if (AutoHide.NotAutoHide()) AutoHide.SetAutoHide();
        }

        public void Reset()
        {
            Ready();
            Variable.Taskbars.ResetTaskbars();
            HookBar.SetHook();
        }

        public void Run()
        {
            if (Variable.Taskbars.IsMouseOverTaskbar()) return;

            var foregroundHandle = GetForegroundWindow();
            if (foregroundHandle.IsWindowInvisible()) return;


            if (foregroundHandle.IsClassNameInvalid()) return;

            _sendMessage = false;
            if (foregroundHandle.IsNotMaximizeWindow()
                || foregroundHandle.IsNotFullScreenWindow())
            {
                GetWindowRect(foregroundHandle, out var rect);
                foreach (var taskbar in Variable.Taskbars.Where(
                    taskbar => (rect.left < taskbar.Rect.Right
                                && rect.right > taskbar.Rect.Left
                                && rect.top < taskbar.Rect.Bottom
                                && rect.bottom > taskbar.Rect.Top)
                               != taskbar.Intersect))
                {
                    taskbar.Intersect = !taskbar.Intersect;
                    _sendMessage = true;
                }
            }
            else
            {
                var monitor = foregroundHandle.GetMonitor();
                foreach (var taskbar in Variable.Taskbars.Where(taskbar =>
                                                                    taskbar.Monitor == monitor != taskbar.Intersect))
                {
                    taskbar.Intersect = !taskbar.Intersect;
                    _sendMessage = true;
                }
            }

            if (!_sendMessage) return;


            foreach (var taskbar in Variable.Taskbars.Where(taskbar => !taskbar.Intersect))
            {
                taskbar.Monitor.PostMesssageShowBar();
                return;
            }

            ShowBar.PostMessageHideBar();
        }
    }
}
