using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartTaskbar.Tray
{
    public partial class MainNotifyIcon : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly IContainer _container;
        private readonly UserConfigEngine _userConfigEngine;

        public MainNotifyIcon(IContainer container, UserConfigEngine usercConfigEngine)
        {
            _container = container;
            _userConfigEngine = usercConfigEngine;


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
