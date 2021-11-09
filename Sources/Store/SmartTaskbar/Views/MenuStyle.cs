using System.Drawing.Drawing2D;

namespace SmartTaskbar;

//https://stackoverflow.com/questions/32786250/windows-10-styled-contextmenustrip
/// <summary>
///     Tray menu style (will be rewritten later)
/// </summary>
internal class Win11ColorTable : ProfessionalColorTable
{
    public override Color MenuItemBorder
        => Color.WhiteSmoke;

    public override Color MenuItemSelected
        => Color.WhiteSmoke;

    public override Color ImageMarginGradientBegin
        => Color.White;

    public override Color ImageMarginGradientMiddle
        => Color.White;

    public override Color ImageMarginGradientEnd
        => Color.White;

}

internal class Win11Renderer : ToolStripProfessionalRenderer
{
    private static int _x;

    public Win11Renderer() : base(new Win11ColorTable())
    {
        var g = Graphics.FromHwnd(IntPtr.Zero);
        _x = (int) Math.Floor(-0.03 * g.DpiX);
    }

    protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
    {
        // Calculate and draw ✓ , adding DPI perception
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
        r.Inflate(_x, _x);
        r.Offset(-_x, 0);
        e.Graphics.DrawLines(Pens.Black,
                             new[]
                             {
                                 new Point(r.Left, r.Bottom - r.Height / 2),
                                 new Point(r.Left + r.Width / 3, r.Bottom),
                                 new Point(r.Right, r.Top)
                             });
    }
}
