using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.UI.Languages;
using SmartTaskbar.UI.ViewModels;

namespace SmartTaskbar.UI.Views
{
    public class MainNotifyIcon : ApplicationContext
    {
        private readonly IContainer _container;

        private readonly Lazy<MainContextMenu> _contextMenuLazy;
        private readonly CultureResource _cultureResource;
        private readonly MainNotifyIconViewModel _mainNotifyIconViewModel;
        private readonly NotifyIcon _notifyIcon;
        private readonly UserConfigEngine _userConfigEngine;

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

        protected override void Dispose(bool disposing)
        {
            // todo 
        }
    }
}
