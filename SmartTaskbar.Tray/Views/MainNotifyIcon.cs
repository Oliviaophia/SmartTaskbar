using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.Tray.Languages;
using SmartTaskbar.Tray.ViewModels;

namespace SmartTaskbar.Tray.Views
{
    public class MainNotifyIcon : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly IContainer _container;
        private readonly UserConfigEngine _userConfigEngine;
        private readonly CultureResource _cultureResource;
        private readonly MainNotifyIconViewModel _mainNotifyIconViewModel;

        private readonly Lazy<MainContextMenu> _contextMenuLazy;

        public MainNotifyIcon(IContainer container, UserConfigEngine userConfigEngine, CultureResource cultureResource)
        {
            _container = container;
            _userConfigEngine = userConfigEngine;
            _cultureResource = cultureResource;
            _mainNotifyIconViewModel = userConfigEngine.InitViewModel<MainNotifyIconViewModel>();

            _contextMenuLazy = new Lazy<MainContextMenu>(
                () => new MainContextMenu(container, userConfigEngine, cultureResource),
                LazyThreadSafetyMode.ExecutionAndPublication);

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
                    // show Menu
                    _contextMenuLazy.Value.Show();
            };

            #endregion
        }

        private void UpdateTheme() { _notifyIcon.Icon = _mainNotifyIconViewModel.Icon; }
    }
}
