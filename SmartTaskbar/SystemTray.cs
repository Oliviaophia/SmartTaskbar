using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SmartTaskbar.Infrastructure;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notify;

        public SystemTray()
        {
            notify = new NotifyIcon
            {
                Text = Application.ProductName,
                Icon = Properties.Resources.logo_32,
                Visible = true
            };
            notify.Click += (s, e) =>
            {
                TaskbarSwitcher.Instance.SwitchTaskbar();
            };
        }
    }
}
