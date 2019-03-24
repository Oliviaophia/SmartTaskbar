using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Languages;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    internal class SystemTray : ApplicationContext
    {
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _settings;


        public SystemTray()
        {
            #region Initialization

            var font = new Font("Segoe UI", 9F);
            _settings = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString("menu_settings"),
                Font = font
            };
            _exit = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString("menu_exit"),
                Font = font
            };
            _contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };

            _contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _settings,
                new ToolStripSeparator(),
                _exit
            });

            _notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = _contextMenuStrip,
                Text = Application.ProductName,
                Icon = Resources.logo_small,
                Visible = true
            };

            #endregion

            #region LoadEvent

            _settings.Click += (s, e) => SettingsView.Get.ChangeDisplayStatus();

            _exit.Click += (s, e) =>
            {
                _notifyIcon.Dispose();
                Application.Exit();
            };

            #endregion
        }
    }
}