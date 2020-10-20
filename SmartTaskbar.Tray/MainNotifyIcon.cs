using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.Tray.ViewModels;

namespace SmartTaskbar.Tray
{
    public class MainNotifyIcon : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly IContainer _container;
        private readonly UserConfigEngine _userConfigEngine;
        private readonly MainNotifyIconViewModel _mainNotifyIconViewModel;

        public MainNotifyIcon(IContainer container, UserConfigEngine userConfigEngine)
        {
            _container = container;
            _userConfigEngine = userConfigEngine;
            _mainNotifyIconViewModel = userConfigEngine.InitViewModel<MainNotifyIconViewModel>();

            #region Initialization

            _notifyIcon = new NotifyIcon(container)
            {
                Text = Constants.ApplicationName,
                Icon = _mainNotifyIconViewModel.Icon,
                Visible = true
            };

            _notifyIcon.MouseClick += (s, e) =>
            {
                // todo
                UpdateTheme();

                if (e.Button == MouseButtons.Right)
                {
                    // show Menu
                }
            };

            #endregion
        }

        private void UpdateTheme() { _notifyIcon.Icon = _mainNotifyIconViewModel.Icon; }
    }
}
