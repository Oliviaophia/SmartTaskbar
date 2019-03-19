using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class TaskbarBuilder
    {
        private static IntPtr _nextTaskbar;
        private static Rectangle _rectangle;
        private static Screen _monitor;
        private static TagRect _tagRect;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IList<Taskbar> UpdateTaskbarList(this IList<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(new Taskbar(FindWindow("Shell_TrayWnd", null)));

            while (true)
            {
                _nextTaskbar = FindWindowEx(IntPtr.Zero, _nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (_nextTaskbar == IntPtr.Zero) return taskbars;

                taskbars.Add(new Taskbar(_nextTaskbar));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Rectangle AdjustRect(this IntPtr handle)
        {
            // todo: have a bug here:
            GetWindowRect(handle, out _tagRect);
            _rectangle = _tagRect;

            _monitor = Screen.FromHandle(handle);
            if (_monitor.Bounds.Bottom < _rectangle.Bottom)
            {
                _rectangle.Offset(0, _monitor.Bounds.Bottom - _rectangle.Bottom);
                return _rectangle;
            }

            if (_monitor.Bounds.Top > _rectangle.Top)
            {
                _rectangle.Offset(0, _monitor.Bounds.Top - _rectangle.Top);
                return _rectangle;
            }

            if (_monitor.Bounds.Left > _rectangle.Left)
            {
                _rectangle.Offset(_monitor.Bounds.Left - _rectangle.Left, 0);
                return _rectangle;
            }

            if (_monitor.Bounds.Right < _rectangle.Right)
            {
                _rectangle.Offset(_monitor.Bounds.Right - _rectangle.Right, 0);
                return _rectangle;
            }

            return _rectangle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IList<Taskbar> UpdateInersect(this IList<Taskbar> taskbars, Func<Taskbar, bool> func)
        {
            foreach (var taskbar in taskbars) taskbar.IsIntersect = func(taskbar);

            return taskbars;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowTaskbarbyInersect(this IList<Taskbar> taskbars)
        {
            foreach (var taskbar in taskbars)
            {
                if (taskbar.IsIntersect) continue;

                taskbar.Monitor.PostMesssageShowTaskbar();
                return;
            }

            ShowTaskbar.PostMessageHideTaskbar();
        }
    }
}