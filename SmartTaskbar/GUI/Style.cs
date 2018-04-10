using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartTaskbar
{
    //https://stackoverflow.com/questions/32786250/windows-10-styled-contextmenustrip
    public class Win10ColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => Color.WhiteSmoke;

        public override Color MenuItemSelected => Color.WhiteSmoke;

        //public override Color ToolStripDropDownBackground => Color.White;

        public override Color ImageMarginGradientBegin => Color.White;

        public override Color ImageMarginGradientMiddle => Color.White;

        public override Color ImageMarginGradientEnd => Color.White;
    }

    public class Win10Renderer : ToolStripProfessionalRenderer
    {
        public Win10Renderer() : base(new Win10ColorTable()){}
        //protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        //{
        //    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //    var r = new Rectangle(e.ArrowRectangle.Location, e.ArrowRectangle.Size);
        //    r.Inflate(-2, -6);
        //    e.Graphics.DrawLines(Pens.Black, new Point[]{
        //        new Point(r.Left, r.Top),
        //        new Point(r.Right, r.Top + r.Height / 2),
        //        new Point(r.Left, r.Top + r.Height)});
        //}

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            r.Inflate(-4, -6);
            e.Graphics.DrawLines(Pens.Black, new Point[]{
                new Point(r.Left, r.Bottom - r.Height / 2),
                new Point(r.Left + r.Width / 3,  r.Bottom),
                new Point(r.Right, r.Top)});
        }
    }
}
