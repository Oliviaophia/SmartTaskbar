using System.Text;

namespace SmartTaskbar;

public static partial class Fun
{
    private const int Capacity = 256;
    private static readonly StringBuilder Sb = new(Capacity);

    public static string GetName(this in IntPtr handle)
        => GetClassName(handle, Sb.Clear(), Capacity) == 0 ? "" : Sb.ToString();
}
