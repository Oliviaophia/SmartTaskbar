namespace SmartTaskbar
{
    /// <summary>
    ///     Taskbar information structure
    /// </summary>
    public readonly struct TaskbarInfo
    {
        public static readonly TaskbarInfo Empty = new();

        /// <summary>
        ///     Initialize taskbar information
        /// </summary>
        /// <returns></returns>
        public TaskbarInfo(IntPtr handle, TagRect rect, bool isShow, TaskbarPosition position, IntPtr monitor)
        {
            Handle = handle;
            Rect = rect;
            IsShow = isShow;
            Position = position;
            Monitor = monitor;
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

        /// <summary>
        ///     Taskbar Monitor
        /// </summary>
        public readonly IntPtr Monitor;
    }
}
