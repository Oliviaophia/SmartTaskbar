using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Model;

namespace SmartTaskbar.Views
{
    public partial class MainSettingForm : Form
    {
        private readonly CoreInvoker _coreInvoker;
        private readonly IContainer _container;

        public MainSettingForm(IContainer container, CoreInvoker coreInvoker)
        {
            _container = container;
            _coreInvoker = coreInvoker;
            InitializeComponent();

            VisibleChanged += (s, e) =>
            {
                if (Visible) Activate();
            };
            Activated += (s, e) => ChangeTheme();
            Deactivate += (s, e) => Hide();

            #region Initialization

            groupBoxState0.Text = coreInvoker.GetText("SettTaskbarState0");
            groupBoxState1.Text = coreInvoker.GetText("SettTaskbarState1");
            groupBoxState2.Text = coreInvoker.GetText("SettTaskbarState2");

            groupBoxTransparentMode0.Text =
                groupBoxTransparentMode1.Text =
                    groupBoxTransparentMode2.Text =
                        coreInvoker.GetText("SettTransparentMode");

            checkBoxIsAutoHide0.Text =
                checkBoxIsAutoHide1.Text =
                    checkBoxIsAutoHide2.Text =
                        coreInvoker.GetText("SettIsAutoHide");

            checkBoxHideTaskbar0.Text =
                checkBoxHideTaskbar1.Text =
                    checkBoxHideTaskbar2.Text = 
                        coreInvoker.GetText("SettHideTaskbar");

            checkBoxIconSize0.Text =
                checkBoxIconSize1.Text =
                    checkBoxIconSize2.Text =
                        coreInvoker.GetText("SettIconSize");

            radioButtonDisable0.Text =
                radioButtonDisable1.Text =
                    radioButtonDisable2.Text =
                        coreInvoker.GetText("SettDisable");

            radioButtonTransparent0.Text =
                radioButtonTransparent1.Text =
                    radioButtonTransparent2.Text =
                        coreInvoker.GetText("SettTransparent");

            radioButtonBlur0.Text =
                radioButtonBlur1.Text =
                    radioButtonBlur2.Text =
                        coreInvoker.GetText("SettBlur");

            #endregion
        }


        private void ChangeTheme()
        {
            Icon = _coreInvoker.GetIcon();

            var islight = InvokeMethods.IsLightTheme();

            BackColor = islight ? Color.FromArgb(238, 238, 238) : Color.FromArgb(43, 43, 43);
            ForeColor = islight ? Color.Black : Color.White;

            // todo 

        }
    }
}