using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class BarBuilder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IList<Taskbar> UpdateTaskbarList(this IList<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(new Taskbar(FindWindow("Shell_TrayWnd", null)));

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (nextTaskbar == IntPtr.Zero) return taskbars;

                taskbars.Add(new Taskbar(nextTaskbar));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IList<Taskbar> UpdateInersect(this IList<Taskbar> taskbars, Func<Taskbar, bool> func)
        {
            foreach (var taskbar in taskbars) taskbar.Intersect = func(taskbar);

            return taskbars;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowTaskbarbyInersect(this IList<Taskbar> taskbars)
        {
            foreach (var taskbar in taskbars)
            {
                if (taskbar.Intersect) continue;

                taskbar.Monitor.PostMesssageShowBar();
                return;
            }

            ShowBar.PostMessageHideBar();
        }
    }
}