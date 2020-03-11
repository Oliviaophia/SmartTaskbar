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