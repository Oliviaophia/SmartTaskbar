using SmartTaskbar.Core.Helpers;
using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class ForegroundMode : IAutoMode
    {
        private static IntPtr foregroundHandle;
        private static IntPtr monitor;
        private static TAGRECT rect;

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
                taskbars.UpdateInersect(_ => _.Monitor == monitor);
            }
            else
            {
                GetWindowRect(foregroundHandle, out rect);
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
