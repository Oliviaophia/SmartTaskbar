using System.Windows.Forms;
using Windows.UI.ViewManagement;
using SmartTaskbar.Engines;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.UI.Languages;

namespace SmartTaskbar.UI.Views
{
    public partial class MainSettingForm : Form
    {
        private readonly CultureResource _cultureResource;
        private readonly UserConfigEngine<MainViewModel> _userConfigEngine;

        public MainSettingForm(UserConfigEngine<MainViewModel> userConfigEngine,
                               CultureResource                 cultureResource)
        {
            _userConfigEngine = userConfigEngine;
            _cultureResource = cultureResource;
            InitializeComponent();
            UpdateTheme();

            UIInfo.Settings.ColorValuesChanged += Settings_ColorValuesChanged;
        }

        private void Settings_ColorValuesChanged(UISettings sender, object args)
        {
            if (InvokeRequired)
                BeginInvoke(new MethodInvoker(UpdateTheme));
            else
                UpdateTheme();
        }

        public void BringUp()
        {
            if (Visible)
            {
                WindowState = FormWindowState.Normal;
                Activate();
                Focus();
            }
            else { Show(); }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            base.OnFormClosing(e);
        }

        private void UpdateTheme()
        {
            Icon = _userConfigEngine.ViewModel.Icon;
            BackColor = UIInfo.Background;
            ForeColor = UIInfo.ForeGround;
            panelMenu.BackColor = UIInfo.IsWhiteBackground ? UIInfo.AccentLight3 : UIInfo.AccentDark1;
        }
    }
}
