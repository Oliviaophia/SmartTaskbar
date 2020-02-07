using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;
using SmartTaskbar.Properties;

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
                Icon = GetIcon(),
                Visible = true
            };

            _notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Right)
                    return;

                MainContextMenuInstance.Show();
                MainContextMenuInstance.Activate();
            };

            #endregion
        }

        private Icon GetIcon() =>
            _coreInvoker.UserSettings.IconStyle switch
            {
                IconStyle.Black => Resources.Logo_Black,
                IconStyle.Blue => Resources.Logo_Blue,
                IconStyle.Pink => Resources.Logo_Pink,
                IconStyle.White => Resources.Logo_White,
                IconStyle.Auto => InvokeMethods.IsLightTheme()
                    ? Resources.Logo_Black
                    : Resources.Logo_White,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}