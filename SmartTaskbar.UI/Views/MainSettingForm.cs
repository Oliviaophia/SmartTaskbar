using System.Windows.Forms;
using Microsoft.Win32;
using SmartTaskbar.Engines;
using SmartTaskbar.UI.Languages;

namespace SmartTaskbar.UI.Views
{
    public partial class MainSettingForm : Form
    {
        private readonly UserConfigEngine<MainViewModel> _userConfigEngine;
        private readonly CultureResource _cultureResource;

        public MainSettingForm(UserConfigEngine<MainViewModel> userConfigEngine,
                               CultureResource                 cultureResource)
        {
            _userConfigEngine = userConfigEngine;
            _cultureResource = cultureResource;
            InitializeComponent();
            ThemeUpdate();

            SystemEvents.UserPreferenceChanged += (s, e) => ThemeUpdate();
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

        private void ThemeUpdate() { Icon = _userConfigEngine.ViewModel.Icon; }
    }
}
