using System;
using System.Collections.Generic;

namespace SmartTaskbar.Core
{
    internal static class Variable
    {
        internal static List<Taskbar> taskbars = new List<Taskbar>(4);

        internal static Dictionary<IntPtr, string> nameCache = new Dictionary<IntPtr, string>(64);
    }
}
