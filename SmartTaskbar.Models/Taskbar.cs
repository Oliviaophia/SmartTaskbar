using System;
using System.Drawing;

namespace SmartTaskbar.Models
{
    public struct Taskbar
    {
        public IntPtr Handle { get; set; }

        public IntPtr Monitor { get; set; }

        public Rectangle Rect { get; set; }

        public bool Intersect { get; set; }
    }
}
