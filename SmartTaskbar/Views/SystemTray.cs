using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Languages;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    internal class SystemTray : ApplicationContext
    {
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ToolStripMenuItem _menuAutoDisplay;

        private readonly ToolStripMenuItem _menuAutoSize;
        private readonly ToolStripMenuItem _menuExit;
        private readonly ToolStripMenuItem _menuSettings;
        private readonly NotifyIcon _notifyIcon;


        public SystemTray()
        {
            #region Initialization

            var font = new Font("Segoe UI", 9F);
            _menuSettings = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(_menuSettings)),
                Font = font
            };
            _menuAutoDisplay = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(_menuAutoDisplay)),
                Font = font
            };
            _menuAutoSize = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(_menuAutoSize)),
                Font = font
            };
            _menuExit = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(_menuExit)),
                Font = font
            };
            _contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };

            _contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _menuSettings,
                new ToolStripSeparator(),
                _menuAutoDisplay,
                _menuAutoSize,
                new ToolStripSeparator(),
                _menuExit
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

            _menuSettings.Click += (s, e) => SettingsView.Get.ChangeDisplayStatus();

            _menuExit.Click += (s, e) =>
            {
                _notifyIcon.Dispose();
                Application.Exit();
            };

            #endregion
        }
    }
}