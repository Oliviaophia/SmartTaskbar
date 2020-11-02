using System;
using System.Collections.Generic;

namespace SmartTaskbar.Core
{
    internal static class Variable
    {
        internal static List<Taskbar> Taskbars = new List<Taskbar>(4);

        internal static Dictionary<IntPtr, string> NameCache = new Dictionary<IntPtr, string>(16);
    }
}
