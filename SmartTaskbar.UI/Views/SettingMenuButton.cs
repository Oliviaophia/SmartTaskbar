using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.PlatformInvoke;

namespace SmartTaskbar.UI.Views
{
    public sealed class SettingMenuButton : Button
    {
        public SettingMenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Transparent;
            Size = new Size(180, 32);
            Margin = Padding.Empty;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 12F);
            TextAlign = ContentAlignment.MiddleLeft;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleCenter;
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
            FlatAppearance.MouseOverBackColor =
                UIInfo.IsWhiteBackground ? UIInfo.AccentLight1 : UIInfo.AccentDark3;
            FlatAppearance.MouseDownBackColor =
                UIInfo.IsWhiteBackground ? UIInfo.AccentLight2 : UIInfo.AccentDark2;
        }
    }
}
