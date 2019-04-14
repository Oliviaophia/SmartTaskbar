using System.Runtime.CompilerServices;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class ForegroundMode : IAutoMode
    {
        private static bool _sendMessage;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ForegroundMode()
        {
            Ready();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ready()
        {
            _sendMessage = true;
            if (AutoHide.NotAutoHide()) AutoHide.SetAutoHide();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            if (Variable.taskbars.IsMouseOverTaskbar()) return;

            var foregroundHandle = GetForegroundWindow();
            if (foregroundHandle.IsWindowInvisible()) return;


            if (foregroundHandle.IsClassNameInvalid()) return;

            if (foregroundHandle.IsNotMaximizeWindow())
            {
                GetWindowRect(foregroundHandle, out TagRect rect);
                Variable.taskbars.UpdateInersect(out _sendMessage, _ =>
                    rect.left < _.Rect.Right &&
                    rect.right > _.Rect.Left &&
                    rect.top < _.Rect.Bottom &&
                    rect.bottom > _.Rect.Top);
            }
            else
            {
                var monitor = foregroundHandle.GetMonitor();
                Variable.taskbars.UpdateInersect(out _sendMessage, _ => _.Monitor == monitor);
            }

            if (_sendMessage) Variable.taskbars.ShowTaskbarbyInersect();
        }
    }
}