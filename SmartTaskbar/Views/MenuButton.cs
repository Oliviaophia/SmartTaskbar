using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;

namespace SmartTaskbar.Views
{
    public sealed class MenuButton : Button
    {
        public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Transparent;
            Size = new Size(230, 40);
            Margin = Padding.Empty;
            FlatAppearance.BorderSize = 0;
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
            var isLight = InvokeMethods.IsLightTheme();
            ForeColor = ForeColor = isLight ? Color.Black : Color.White;
            FlatAppearance.MouseOverBackColor = FlatAppearance.MouseDownBackColor = isLight ? Color.White : Color.FromArgb(65, 65, 65);
        }
    }
}
