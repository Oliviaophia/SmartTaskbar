using System;
using System.Drawing;
using SmartTaskbar.Core.Helpers;

namespace SmartTaskbar.Core
{
    internal class Taskbar
    {
        public Taskbar(IntPtr handle) => (Handle, Monitor, Rect) = (handle, handle.GetMonitor(), handle.AdjustRect());
        public IntPtr Handle { get; }

        public IntPtr Monitor { get; }

        public Rectangle Rect { get; }

        public bool IsIntersect { get; set; }
    }
}