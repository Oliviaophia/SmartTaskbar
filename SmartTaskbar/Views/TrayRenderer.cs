using System.Windows.Forms;

namespace SmartTaskbar.Views
{
    internal class TrayRenderer : ToolStripProfessionalRenderer
    {
        public TrayRenderer() : base(new TrayColorTable())
        {
        }
    }
}