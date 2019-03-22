using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class MouseHover
    {
        private static IntPtr _lastHandle;
        private static IntPtr _currentHandle;
        private static bool _lastResult;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsMouseOverTaskbar(this IList<Taskbar> taskbars)
        {
            GetCursorPos(out TagPoint point);
            _currentHandle = WindowFromPoint(point);
            if (_lastHandle == _currentHandle) return _lastResult;

            var monitor = _currentHandle.GetMonitor();
            var taskbar = taskbars.Where(_ => _.Monitor == monitor).Select(_ => _.Handle).FirstOrDefault();
            if (taskbar == IntPtr.Zero) return _lastResult = false;

            _lastHandle = _currentHandle;
            var desktopHandle = GetDesktopWindow();
            while (_currentHandle != desktopHandle)
            {
                if (taskbar == _currentHandle) return _lastResult = true;

                _currentHandle = _currentHandle.GetParentWindow();
            }

            return _lastResult = false;
        }
    }
}