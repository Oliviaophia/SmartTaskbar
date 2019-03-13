using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region Taskbar

        public static void UpdateTaskbarList()
        {
            taskbars.Clear();
            taskbars.Add(new Taskbar(FindWindow("Shell_TrayWnd", null)));

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (nextTaskbar == IntPtr.Zero)
                {
                    return;
                }

                taskbars.Add(new Taskbar(nextTaskbar));
            }
        }

        #endregion


        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessageW(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region AutoMode

        public static bool IsMouseOverTaskbar()
        {
            GetCursorPos(out point);
            intPtr = GetDesktopWindow();
            windowHandles.Clear();

            windowHandles.Add(WindowFromPoint(point));
            while (windowHandles.Last() != intPtr)
            {
                windowHandles.Add(windowHandles.Last().GetParentWindow());
            }

            foreach (var taskbar in taskbars)
            {
                if (windowHandles.Contains(taskbar.Handle))
                {
                    return true;
                }
            }

            return false;
        }



        public static void InvokeForeGroundMode()
        {
            if (IsMouseOverTaskbar())
            {
                return;
            }

            intPtr = GetForegroundWindow();

            if (IsWindowVisible(intPtr) == false)
            {
                return;
            }

            DwmGetWindowAttribute(intPtr, 14, out cloakedval, sizeof(int));
            if (cloakedval)
            {
                return;
            }


            sb.Clear();
            GetClassName(intPtr, sb, 255);

            string name = sb.ToString();

            if (name == "WorkerW" ||
                name == "Progman" ||
                name == "DV2ControlHost" ||
                name == "Shell_TrayWnd" ||
                name == "Shell_SecondaryTrayWnd" ||
                name == "MultitaskingViewFrame" ||
                name == "Windows.UI.Core.CoreWindow"   // todo
                )
            {
                return;
            }

            if (intPtr.IsMaxWindow())
            {
                var monitor = intPtr.GetMonitor();

                foreach (var taskbar in taskbars)
                {
                    taskbar.IsIntersect = taskbar.Monitor == monitor;
                }
            }
            else
            {
                GetWindowRect(intPtr, out rect);
                foreach (var taskbar in taskbars)
                {
                    taskbar.IsIntersect = Intersect(rect, taskbar.Rect);
                }
            }

            var t = taskbars.FirstOrDefault(_ => !_.IsIntersect);
            if (t is null)
            {
                PostMessageHideTaskbar();
                return;
            }

            t.Monitor.PostMesssageShowTaskbar();
        }

        internal static bool Intersect(Rectangle rect1, Rectangle rect2) => rect1.IntersectsWith(rect2);
        #endregion
    }
}
