using SmartTaskbar.Core.Helpers;
using System;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class DefaultMode : IAutoMode
    {
        private static IntPtr foregroundHandle;
        private static IntPtr monitor;
        private static TAGRECT rect;
        private const int offset = 8;

        public DefaultMode()
        {
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            taskbars.UpdateTaskbarList();
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
                    rect.left + offset < _.Rect.Right &&
                    rect.right + offset > _.Rect.Left &&
                    rect.top + offset < _.Rect.Bottom &&
                    rect.bottom + offset > _.Rect.Top);
            }

            taskbars.ShowTaskbarbyInersect();
        }
    }
}
