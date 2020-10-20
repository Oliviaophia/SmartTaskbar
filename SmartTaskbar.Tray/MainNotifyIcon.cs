using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;

namespace SmartTaskbar.Tray
{
    public class MainNotifyIcon : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly IContainer _container;
        private readonly UserConfigEngine _userConfigEngine;

        public MainNotifyIcon(IContainer container, UserConfigEngine userConfigEngine)
        {
            _container = container;
            _userConfigEngine = userConfigEngine;


            #region Initialization

            _notifyIcon = new NotifyIcon(container)
            {
                Text = Constants.ApplicationName,
                Icon = IconResource.Logo_Blue,
                Visible = true
            };
            #endregion
        }

    }
}
