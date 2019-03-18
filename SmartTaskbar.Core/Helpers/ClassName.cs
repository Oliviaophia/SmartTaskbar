using System;
using System.Runtime.CompilerServices;
using System.Text;

using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ClassName
    {
        private const int capacity = 255;
        private static readonly StringBuilder stringBuilder = new StringBuilder(capacity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsClassNameInvalid(this IntPtr handle)
        {
            stringBuilder.Clear();

            GetClassName(handle, stringBuilder, capacity);

            switch (stringBuilder.ToString())
            {
                case "WorkerW":
                case "Progman":
                case "DV2ControlHost":
                case "Shell_TrayWnd":
                case "Shell_SecondaryTrayWnd":
                case "MultitaskingViewFrame":
                    return true;
                // todo: have a bug here:
                //case "Windows.UI.Core.CoreWindow":
                //    return true;
                default:
                    return false;
            }
        }
    }
}
