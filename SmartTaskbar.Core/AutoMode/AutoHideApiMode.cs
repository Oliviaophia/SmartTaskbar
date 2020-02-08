using System;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.AutoMode
{
    public class AutoHideApiMode : IAutoMode
    {
        private static IntPtr _maxWindow;
        private static bool _tryShowBar;

        public AutoHideApiMode()
        {
            Ready();
        }

        public void Run()
        {
            if (_maxWindow != IntPtr.Zero)
            {
                if (_maxWindow.IsWindowInvisible() || _maxWindow.IsNotMaximizeWindow()) Ready();
                return;
            }

            if (Variable.Taskbars.IsMouseOverTaskbar()) return;

            _maxWindow = IntPtr.Zero;
            EnumWindows((handle, lp) =>
            {
                if (handle.IsWindowInvisible()) return true;

                if (handle.IsNotMaximizeWindow()) return true;

                _maxWindow = handle;
                return false;
            }, IntPtr.Zero);

            if (_maxWindow == IntPtr.Zero)
            {
                if (_tryShowBar == false) return;
                _tryShowBar = false;

                // todo 
                return;
            }

            // todo 
        }

        public void Ready()
        {
            _maxWindow = IntPtr.Zero;
            _tryShowBar = true;
        }

        public void Reset()
        {
            Ready();
            Variable.Taskbars.ResetTaskbars();
        }
    }
}
