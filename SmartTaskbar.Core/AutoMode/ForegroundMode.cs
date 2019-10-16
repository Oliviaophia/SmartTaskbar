using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class ForegroundMode : IAutoMode
    {
        // Do not use Higher order function or Lambda here; Advanced syntax is not used here for performance reasons;

        private static bool _sendMessage;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ForegroundMode()
        {
            Ready();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ready()
        {
            if (AutoHide.NotAutoHide()) AutoHide.SetAutoHide();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            if (Variable.Taskbars.IsMouseOverTaskbar()) return;

            var foregroundHandle = GetForegroundWindow();
            if (foregroundHandle.IsWindowInvisible()) return;


            if (foregroundHandle.IsClassNameInvalid()) return;

            _sendMessage = false;
            if (foregroundHandle.IsNotMaximizeWindow())
            {
                GetWindowRect(foregroundHandle, out TagRect rect);
                foreach (var taskbar in Variable.Taskbars)
                {
                    if ((rect.left < taskbar.Rect.Right &&
                         rect.right > taskbar.Rect.Left &&
                         rect.top < taskbar.Rect.Bottom &&
                         rect.bottom > taskbar.Rect.Top) == taskbar.Intersect) continue;

                    taskbar.Intersect = !taskbar.Intersect;
                    _sendMessage = true;
                }
            }
            else
            {
                var monitor = foregroundHandle.GetMonitor();
                foreach (var taskbar in Variable.Taskbars)
                {
                    if (taskbar.Monitor == monitor == taskbar.Intersect) continue;

                    taskbar.Intersect = !taskbar.Intersect;
                    _sendMessage = true;
                }
            }

            if (!_sendMessage) return;


            foreach (var taskbar in Variable.Taskbars)
            {
                if (taskbar.Intersect) continue;

                taskbar.Monitor.PostMesssageShowBar();
                return;
            }

            ShowBar.PostMessageHideBar();
        }
    }
}