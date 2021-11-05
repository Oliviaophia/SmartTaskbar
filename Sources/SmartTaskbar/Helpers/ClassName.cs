using System.Text;

namespace SmartTaskbar;

using static NativeMethods;

internal static class ClassName
{
    private const int Capacity = 256;
    private static readonly StringBuilder Sb = new(Capacity);

    internal static string GetName(this IntPtr handle)
    {
        _ = Sb.Clear();
        _ = GetClassName(handle, Sb, Capacity);

        return Sb.ToString();
    }
}
