using System;
using System.Windows.Forms;

namespace SmartTaskbar.Views
{
    public partial class SettingsView : Form
    {
        private static readonly Lazy<SettingsView> _instance = new Lazy<SettingsView>(() => new SettingsView());

        internal static SettingsView Instance => _instance.Value;

        private SettingsView()
        {
            InitializeComponent();
            FormClosing += (s, e) =>
            {
                if (e.CloseReason != CloseReason.UserClosing) return;

                e.Cancel = true;
                Visible = false;
            };
        }

        internal void ShowView() => Visible = true;

        internal void ChangeDisplayStatus() => Visible = !Visible;
    }
}
