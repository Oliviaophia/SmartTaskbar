using System;
using System.Drawing;

namespace SmartTaskbar.Models
{
    public record Taskbar(IntPtr Handle, IntPtr Monitor, Rectangle Rect, bool Intersect);
}
