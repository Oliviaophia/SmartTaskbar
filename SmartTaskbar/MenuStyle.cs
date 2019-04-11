using System.Drawing;
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
    }
}