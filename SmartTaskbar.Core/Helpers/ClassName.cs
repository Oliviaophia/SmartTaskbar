using System;
using System.Runtime.CompilerServices;
using System.Text;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ClassName
    {
        private const int Capacity = 256;
        private static readonly StringBuilder StringBuilder = new StringBuilder(Capacity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsClassNameInvalid(this IntPtr handle)
        {
            StringBuilder.Clear();

            GetClassName(handle, StringBuilder, Capacity);

            switch (StringBuilder.ToString())
            {
                case "WorkerW":
                case "Progman":
                case "DV2ControlHost":
                case "Shell_TrayWnd":
                case "Shell_SecondaryTrayWnd":
                case "MultitaskingViewFrame":
                // todo: have a bug here:
                case "Windows.UI.Core.CoreWindow":
                    return true;
                default:
                    return false;
            }
        }
    }
}