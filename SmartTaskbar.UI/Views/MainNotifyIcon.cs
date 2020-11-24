using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.UI.Languages;

namespace SmartTaskbar.UI.Views
{
    public class MainNotifyIcon : ApplicationContext
    {
        private readonly IContainer _container;

        private readonly Lazy<MainContextMenu> _contextMenuLazy;
        private readonly CultureResource _cultureResource;
        private readonly NotifyIcon _notifyIcon;
        private readonly UserConfigEngine<MainViewModel> _userConfigEngine;

        public MainNotifyIcon(UserConfigEngine<MainViewModel> userConfigEngine,
                              CultureResource                 cultureResource)
        {
            _container = new Container();
            _userConfigEngine = userConfigEngine;
            _cultureResource = cultureResource;

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

            _notifyIcon.MouseClick += (s, e) =>
            {
                // todo
                UpdateTheme();

                if (e.Button == MouseButtons.Right)
                    // show Menu
                    _contextMenuLazy.Value.Show();
            };

            #endregion
        }

        private void UpdateTheme() { _notifyIcon.Icon = _userConfigEngine.ViewModel.Icon; }

        protected override void Dispose(bool disposing) { _container?.Dispose(); }
    }
}
