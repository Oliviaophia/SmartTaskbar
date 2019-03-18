using SmartTaskbar.Core.Helpers;
using System;
using System.Drawing;

namespace SmartTaskbar.Core
{
    internal class Taskbar
    {
        public IntPtr Handle { get; }

        public IntPtr Monitor { get; }

        public Rectangle Rect { get; }

        public bool IsIntersect { get; set; }

        public Taskbar(IntPtr handle) => (Handle, Monitor, Rect) = (handle, handle.GetMonitor(), handle.AdjustRect());
    }
}
