using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Model;

namespace SmartTaskbar.Views
{
    internal class MainNotifyIcon : Form
    {
        private static MainContextMenu _mainContextMenu;
        private readonly CoreInvoker _coreInvoker;
        private readonly NotifyIcon _notifyIcon;
        private readonly IContainer _container;


        public MainContextMenu MainContextMenuInstance
        {
            get
            {
                if (_mainContextMenu == null || _mainContextMenu.IsDisposed)
                    _mainContextMenu = new MainContextMenu(_container, _coreInvoker);

                return _mainContextMenu;
            }
        }


        public MainNotifyIcon(IContainer container, CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;
            _container = container;

            #region Initialization

            _notifyIcon = new NotifyIcon(container)
            {
                Text = Application.ProductName,
                Icon = coreInvoker.GetIcon(),
                Visible = true
            };

            _notifyIcon.MouseClick += (s, e) =>
            {
                _notifyIcon.Icon = coreInvoker.GetIcon();
                if (e.Button != MouseButtons.Right)
                    return;

                MainContextMenuInstance.Show();
            };
            #endregion
        }
    }
}