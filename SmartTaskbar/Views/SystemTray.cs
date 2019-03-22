using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Languages;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    internal class SystemTray : ApplicationContext
    {
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ToolStripMenuItem _autoDisplay;
        private readonly ToolStripMenuItem _autoSize;
        private readonly ToolStripMenuItem _exit;
        private readonly ToolStripMenuItem _settings;
        private readonly NotifyIcon _notifyIcon;


        public SystemTray()
        {
            #region Initialization

            var font = new Font("Segoe UI", 9F);
            _settings = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString("menu_settings"),
                Font = font
            };
            _autoDisplay = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString("menu_auto_display"),
                Font = font
            };
            _autoSize = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString("menu_auto_size"),
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
                _autoDisplay,
                _autoSize,
                new ToolStripSeparator(),
                _exit
            });

            _notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = _contextMenuStrip,
                Text = @"SmartTaskbar v2.0.0",
                Icon = Resources.logo_32,
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