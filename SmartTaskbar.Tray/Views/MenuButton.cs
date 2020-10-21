using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.PlatformInvoke;

namespace SmartTaskbar.Tray.Views
{
    public sealed class MenuButton : Button
    {
        public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Transparent;
            Size = new Size(230, 32);
            Margin = Padding.Empty;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 9F);
            TextAlign = ContentAlignment.MiddleLeft;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleLeft;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            UseVisualStyleBackColor = false;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            ForeColor = UIInfo.ForeGround;
            FlatAppearance.MouseOverBackColor = FlatAppearance.MouseDownBackColor =
                UIInfo.IsLightTheme() ? UIInfo.AccentLight3 : UIInfo.AccentDark3;
        }
    }
}