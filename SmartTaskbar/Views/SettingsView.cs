using System;
using System.Windows.Forms;
using ReactiveUI;
using SmartTaskbar.ViewModels;

namespace SmartTaskbar.Views
{
    public partial class SettingsView : Form, IViewFor<SettingsViewModel>
    {
        private static readonly Lazy<SettingsView> Instance = new Lazy<SettingsView>(() => new SettingsView());

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

        internal static SettingsView Get => Instance.Value;

        public SettingsViewModel ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SettingsViewModel) value;
        }

        internal void ShowView() => Visible = true;

        internal void ChangeDisplayStatus() => Visible = !Visible;
    }
}