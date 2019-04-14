using System;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    internal class Taskbar
    {
        public Taskbar(IntPtr handle)
        {
            Handle = handle;
        }

        public IntPtr Handle { get; set; }

        public IntPtr Monitor { get; set; }

        public Rectangle Rect { get; set; }

        public bool Intersect { get; set; }
    }
}