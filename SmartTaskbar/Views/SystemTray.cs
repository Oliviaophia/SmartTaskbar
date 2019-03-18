using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Languages;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    internal class SystemTray : ApplicationContext
    {
        private readonly NotifyIcon notifyIcon;
        private readonly ContextMenuStrip contextMenuStrip;

        private readonly ToolStripMenuItem menu_auto_size;
        private readonly ToolStripMenuItem menu_auto_display;
        private readonly ToolStripMenuItem menu_exit;
        private readonly ToolStripMenuItem menu_settings;


        public SystemTray()
        {
            #region Initialization

            var font = new Font("Segoe UI", 9F);
            menu_settings = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(menu_settings)),
                Font = font
            };
            menu_auto_display = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(menu_auto_display)),
                Font = font
            };
            menu_auto_size = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(menu_auto_size)),
                Font = font
            };
            menu_exit = new ToolStripMenuItem
            {
                Text = ResourceCulture.Get.GetString(nameof(menu_exit)),
                Font = font
            };
            contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };

            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                menu_settings,
                new ToolStripSeparator(),
                menu_auto_display,
                menu_auto_size,
                new ToolStripSeparator(),
                menu_exit
            });

            notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = @"SmartTaskbar v2.0.0",
                Icon = Resources.logo_32,
                Visible = true
            };

            #endregion

            #region LoadEvent

            menu_settings.Click += (s, e) => SettingsView.Get.ChangeDisplayStatus();

            menu_exit.Click += (s, e) =>
            {
                notifyIcon.Dispose();
                Application.Exit();
            };

            #endregion
        }
    }
}