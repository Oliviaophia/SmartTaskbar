using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartTaskbar.Views
{
    //https://stackoverflow.com/questions/32786250/windows-10-styled-contextmenustrip
    internal class Win10ColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => Color.WhiteSmoke;

        public override Color MenuItemSelected => Color.WhiteSmoke;

        public override Color ImageMarginGradientBegin => Color.White;

        public override Color ImageMarginGradientMiddle => Color.White;

        public override Color ImageMarginGradientEnd => Color.White;
    }

    internal class Win10Renderer : ToolStripProfessionalRenderer
    {
        public Win10Renderer() : base(new Win10ColorTable())
        {
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            r.Inflate(-4, -6);
            e.Graphics.DrawLines(Pens.Black, new[]
            {
                new Point(r.Left, r.Bottom - r.Height / 2),
                new Point(r.Left + r.Width / 3, r.Bottom),
                new Point(r.Right, r.Top)
            });
        }
    }
}