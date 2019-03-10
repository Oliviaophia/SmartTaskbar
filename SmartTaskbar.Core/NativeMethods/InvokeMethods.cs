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
            _taskbars.Clear();
            _taskbars.Add(new Taskbar(FindWindow("Shell_TrayWnd", null)));

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, "Shell_SecondaryTrayWnd", "");
                if (nextTaskbar == IntPtr.Zero)
                {
                    return;
                }

                _taskbars.Add(new Taskbar(nextTaskbar));
            }
        }

        #endregion


        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessageW(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region AutoMode
        public static bool IsMouseOverTaskbar(Point point)
        {
            var handle = WindowFromPoint(point);

            foreach (var taskbar in _taskbars)
            {
                if (handle == taskbar.Handle)
                {
                    return true;
                }
            }

            return false;
        }



        public static void InvokeForeGroundMode(Point point)
        {

            _foreWindow = GetForegroundWindow();

            if (IsWindowVisible(_foreWindow) == false)
            {
                return;
            }

            DwmGetWindowAttribute(_foreWindow, 14, out cloakedval, sizeof(int));
            if (cloakedval)
            {
                return;
            }


            GetClassName(_foreWindow, sb, 255);

            string name = sb.ToString();
            sb.Clear();

            if (name == "WorkerW" ||
                name == "Progman" ||
                name == "DV2ControlHost" ||
                name == "Shell_TrayWnd" ||
                name == "Shell_SecondaryTrayWnd" ||
                name == "Button"
                )
            {
                return;
            }

            if (_foreWindow.IsMaxWindow())
            {
                var monitor = _foreWindow.GetMonitor();

                foreach (var taskbar in _taskbars)
                {
                    taskbar.IsIntersect = taskbar.Monitor == monitor;
                }
            }
            else
            {
                GetWindowRect(_foreWindow, out lpRect);
                foreach (var taskbar in _taskbars)
                {
                    taskbar.IsIntersect = Intersect(lpRect, taskbar.Rect);
                }
            }

            var t = _taskbars.FirstOrDefault(_ => !_.IsIntersect);
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
