using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    internal static class TaskbarHelper
    {
        internal static List<Taskbar> ResetTaskbars(this List<Taskbar> taskbars)
        {
            taskbars.Clear();
            taskbars.Add(FindWindow(Constants.MainTaskbar, null).InitTaskbar());

            var nextTaskbar = IntPtr.Zero;
            while (true)
            {
                nextTaskbar = FindWindowEx(IntPtr.Zero, nextTaskbar, Constants.SubTaskbar, "");
                if (nextTaskbar == IntPtr.Zero) return taskbars;

                taskbars.Add(nextTaskbar.InitTaskbar());
            }
        }


        private static Taskbar InitTaskbar(this IntPtr handle)
        {
            _ = GetWindowRect(handle, out var tagRect);
            Rectangle rectangle = tagRect;
            var monitor = Screen.FromHandle(handle);

            if (rectangle.Width > rectangle.Height)
            {
                // this taskbar is either on the top or bottom:
                var heightΔ = monitor.Bounds.Bottom - rectangle.Bottom;

                // this taskbar is on the bottom of this monitor:
                if (heightΔ == 0) return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

                // this taskbar is on the bottom of this monitor (hide):
                if (heightΔ == 2 - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // this taskbar is on the top of the below monitor (hide):
                if (heightΔ == -2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // this taskbar is on the top of this monitor:
                if (heightΔ == monitor.Bounds.Height - rectangle.Height)
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

                // this taskbar is on the top of this monitor (hide):
                if (heightΔ == monitor.Bounds.Height - 2)
                {
                    rectangle.Offset(0, rectangle.Height - 2);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // this taskbar is on the bottom of the above monitor (hide):
                if (heightΔ == 2 + monitor.Bounds.Height - rectangle.Height)
                {
                    rectangle.Offset(0, 2 - rectangle.Height);
                    return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
                }

                // This may be triggered when switching the display monitor
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is either on the left or right:
            var widthΔ = rectangle.Left - monitor.Bounds.Left;

            // this taskbar is on the left of this monitor:
            if (widthΔ == 0) return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

            // this taskbar is on the left of this monitor (hide):
            if (widthΔ == 2 - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is on the right of the left side monitor (hide):
            if (widthΔ == -2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is on the right of this monitor:
            if (widthΔ == monitor.Bounds.Width - rectangle.Width)
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);

            // this taskbar is on the right of this monitor (hide):
            if (widthΔ == monitor.Bounds.Width - 2)
            {
                rectangle.Offset(2 - rectangle.Width, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // this taskbar is on the left of the right side monitor (hide):
            if (widthΔ == 2 + monitor.Bounds.Width - rectangle.Width)
            {
                rectangle.Offset(rectangle.Width - 2, 0);
                return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
            }

            // This may be triggered when switching the display monitor
            return new Taskbar(handle, handle.GetMonitor(), rectangle, default);
        }
    }
}
