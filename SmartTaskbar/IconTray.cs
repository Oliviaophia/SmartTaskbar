using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Properties;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class IconTray : ApplicationContext
    {
        private readonly Container _container;
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _settings;

        public IconTray(Container container)
        {
            _container = container;
            var font = new Font("Segoe UI", 9F);
            _settings = new ToolStripMenuItem
            {
                Text = "Settings",
                Font = font
            };
            _exit = new ToolStripMenuItem
            {
                Text = "Exit",
                Font = font
            };
            _contextMenuStrip = new ContextMenuStrip(_container)
            {
                Renderer = new Win10Renderer()
            };

            _contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _settings,
                new ToolStripSeparator(),
                _exit
            });

            _notifyIcon = new NotifyIcon(_container)
            {
                ContextMenuStrip = _contextMenuStrip,
                Text = Application.ProductName,
                Icon = Resources.logo_small,
                Visible = true
            };

            _settings.Click += Settings_Click;
            _exit.Click += Exit_Click;
            _notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            InvokeMethods.SaveUserConfig();
            Application.Exit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}