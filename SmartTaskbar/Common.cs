using System;

namespace SmartTaskbar
{
    internal static class Common
    {
        internal static bool win10 = Environment.OSVersion.Version.Major.ToString() == "10";
    }
}