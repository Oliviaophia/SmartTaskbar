using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace SmartTaskbar
{
    public partial class SettingsWindow : MetroWindow
    {
        private static readonly Lazy<SettingsWindow> lazy = new Lazy<SettingsWindow>(() => new SettingsWindow());

        public static SettingsWindow SettingsInstance => lazy.Value;

        private SettingsWindow()
        {
            InitializeComponent();
            Closing += (s, e) =>
            {
                Hide();
                e.Cancel = true;
            };
        }

        public void SwitchWindow()
        {
            if (IsVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void ShowWindow() => Show();

        public void HideWindow() => Hide();


    }
}
