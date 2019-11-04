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

            return StringBuilder.ToString() switch
            {
                "WorkerW" => true,
                "Progman" => true,
                "DV2ControlHost" => true,
                "Shell_TrayWnd" => true,
                "Shell_SecondaryTrayWnd" => true,
                "MultitaskingViewFrame" => true,
                "Windows.UI.Core.CoreWindow" => true,
                _ => false
            };
        }
    }
}