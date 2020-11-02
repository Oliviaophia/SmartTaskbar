using System.Drawing;
using System.Windows.Forms;

namespace SmartTaskbar.Views
{
    public sealed class MenuDelimiter : Label
    {
        public MenuDelimiter()
        {
            BorderStyle = BorderStyle.Fixed3D;
            Size = new Size(220, 2);
            Margin = new Padding(5);
        }
    }
}
