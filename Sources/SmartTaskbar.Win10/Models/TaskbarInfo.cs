using System;
using static SmartTaskbar.Fun;

namespace SmartTaskbar
{
    /// <summary>
    ///     Taskbar information structure
    /// </summary>
    public readonly struct TaskbarInfo
    {
        /// <summary>
        ///     Initialize taskbar information
        /// </summary>
        /// <returns></returns>
        public TaskbarInfo(IntPtr handle, TagRect rect, bool isShow, TaskbarPosition position)
        {
            Handle = handle;
            Rect = rect;
            IsShow = isShow;
            Position = position;
        }

        /// <summary>
        ///     Taskbar handle
        /// </summary>
        public readonly IntPtr Handle;

        /// <summary>
        ///     Taskbar rectangle (in display state)
        /// </summary>
        public readonly TagRect Rect;

        /// <summary>
        ///     Whether the taskbar is displayed
        /// </summary>
        public readonly bool IsShow;

        /// <summary>
        ///     Taskbar Position
        /// </summary>
        public readonly TaskbarPosition Position;
    }
}
