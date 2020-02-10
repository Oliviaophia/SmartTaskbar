using System;
using System.Text;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core.Helpers
{
    internal static class ClassName
    {
        private const int Capacity = 256;
        private static readonly StringBuilder StringBuilder = new StringBuilder(Capacity);

        internal static bool IsClassNameInvalid(this IntPtr handle)
        {
            StringBuilder.Clear();

            GetClassName(handle, StringBuilder, Capacity);

            return StringBuilder.ToString() switch
            {
                "Progman" => true,
                "WorkerW" => true,
                "DV2ControlHost" => true,
                Constant.MainTaskbar => true,
                Constant.SubTaskbar => true,
                "MultitaskingViewFrame" => true,
                "Windows.UI.Core.CoreWindow" => true,
                _ => false
            };
        }
    }
}