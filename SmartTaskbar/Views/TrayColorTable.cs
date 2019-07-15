using System.Drawing;
using System.Windows.Forms;

namespace SmartTaskbar.Views
{
    internal class TrayColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => Color.CornflowerBlue;

        public override Color MenuItemSelected => Color.CornflowerBlue;

        public override Color ToolStripDropDownBackground => Color.GhostWhite;

        public override Color ImageMarginGradientBegin => Color.GhostWhite;

        public override Color ImageMarginGradientMiddle => Color.GhostWhite;

        public override Color ImageMarginGradientEnd => Color.GhostWhite;
    }
}