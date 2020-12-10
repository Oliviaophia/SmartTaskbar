using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Windows.UI.ViewManagement;
using Microsoft.Win32;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.UI.Languages;

namespace SmartTaskbar.UI.Views
{
    public class MainNotifyIcon : ApplicationContext
    {
        private readonly AutoModeWorker _autoModeWorker;
        private readonly IContainer _container;

        private readonly Lazy<MainContextMenu> _contextMenuLazy;
        private readonly NotifyIcon _notifyIcon;
        private readonly TimeEngine _timeEngine;
        private readonly UserConfigEngine<MainViewModel> _userConfigEngine;

        public MainNotifyIcon(UserConfigEngine<MainViewModel> userConfigEngine,
                              CultureResource                 cultureResource,
                              AutoModeWorker                  autoModeWorker,
                              TimeEngine                      timeEngine)
        {
            _container = new Container();
            _userConfigEngine = userConfigEngine;
            _autoModeWorker = autoModeWorker;
            _timeEngine = timeEngine;

            _contextMenuLazy = new Lazy<MainContextMenu>(
                () => new MainContextMenu(userConfigEngine, cultureResource),
                LazyThreadSafetyMode.ExecutionAndPublication);

            #region Initialization

            _notifyIcon = new NotifyIcon(_container)
            {
                Text = Constants.ApplicationName,
                Icon = _userConfigEngine.ViewModel.Icon,
                Visible = true
            };

            _notifyIcon.MouseClick += NotifyIcon_MouseClick;

            UIInfo.Settings.ColorValuesChanged += Settings_ColorValuesChanged;

            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

            #endregion
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            _autoModeWorker.UpdateTaskbarList();
        }

        private void Settings_ColorValuesChanged(UISettings sender, object args) { UpdateTheme(); }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // todo

            if (e.Button == MouseButtons.Right)
            {
                // show Menu
                _contextMenuLazy.Value.Show();
                _contextMenuLazy.Value.Activate();
                _contextMenuLazy.Value.Focus();
            }
        }

        private void UpdateTheme() { _notifyIcon.Icon = _userConfigEngine.ViewModel.Icon; }

        protected override void Dispose(bool disposing)
        {
            _container.Dispose();
            _timeEngine.Dispose();
        }
    }
}
