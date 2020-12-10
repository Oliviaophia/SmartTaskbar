using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Models;

namespace SmartTaskbar.Views
{
    internal class MainNotifyIcon : Form
    {
        private static MainContextMenu _mainContextMenu;
        private readonly CoreInvoker _coreInvoker;
        private readonly NotifyIcon _notifyIcon;


        public MainNotifyIcon(IContainer container, CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;

            #region Initialization

            _notifyIcon = new NotifyIcon(container)
            {
                Text = Application.ProductName,
                Icon = coreInvoker.GetIcon(),
                Visible = true
            };

            _notifyIcon.MouseClick += _notifyIcon_MouseClick;

            #endregion
        }

        public MainContextMenu MainContextMenuInstance
        {
            get
            {
                if (_mainContextMenu == null
                    || _mainContextMenu.IsDisposed)
                    _mainContextMenu = new MainContextMenu(_coreInvoker);

                return _mainContextMenu;
            }
        }

        private void _notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            _notifyIcon.Icon = _coreInvoker.GetIcon();
            if (e.Button != MouseButtons.Right)
                return;

            MainContextMenuInstance.Show();
        }
    }
}
