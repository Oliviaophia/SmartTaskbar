using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.PlatformInvoke;

namespace SmartTaskbar.Tray.Views
{
    public sealed class MenuDelimiter : Label
    {
        public MenuDelimiter()
        {
            BorderStyle = BorderStyle.Fixed3D;
            Size = new Size(220, 2);
            Margin = new Padding(5);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            ForeColor = UIInfo.IsLightTheme() ? UIInfo.AccentLight1 : UIInfo.AccentDark1;
        }
    }
}