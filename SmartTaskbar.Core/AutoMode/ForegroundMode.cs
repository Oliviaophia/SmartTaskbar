using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class ForegroundMode : IAutoMode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ForegroundMode()
        {
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            AutoHide.SetAutoHide();
            ShowTaskbar.PostMessageHideTaskbar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            if (taskbars.IsMouseOverTaskbar()) return;

            var foregroundHandle = GetForegroundWindow();
            if (foregroundHandle.IsWindowInvisible()) return;


            if (foregroundHandle.IsClassNameInvalid()) return;

            if (foregroundHandle.IsMaximizeWindow())
            {
                var monitor = foregroundHandle.GetMonitor();
                taskbars.UpdateInersect(_ => _.Monitor == monitor);
            }
            else
            {
                GetWindowRect(foregroundHandle, out Tagrect rect);
                taskbars.UpdateInersect(_ =>
                    rect.left < _.Rect.Right &&
                    rect.right > _.Rect.Left &&
                    rect.top < _.Rect.Bottom &&
                    rect.bottom > _.Rect.Top);
            }

            taskbars.ShowTaskbarbyInersect();
        }
    }
}