using System.Windows.Forms;
using static SmartTaskbar.Infrastructure.Languages.ResourceCulture;
using static SmartTaskbar.Infrastructure.Switcher.TaskbarSwitcher;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notify;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem menu_about, menu_settings, menu_auto, menu_show, menu_hide, menu_exit;

        public SystemTray()
        {
            var font = new System.Drawing.Font("Segoe UI", 9F);

            menu_about = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_about)),
                Font = font
            };
            menu_about.Click += (s, e) => FormAbout.FormInstance.SwitchWindow();
            menu_settings = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_settings)),
                Font = font
            };
            menu_settings.Click += (s, e) => SettingsWindow.SettingsInstance.SwitchWindow();
            menu_auto = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_auto)),
                Font = font
            };
            menu_auto.Click += (s, e) => SwitcherInstance.DefaultAutoMode();
            menu_show = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_show)),
                Font = font
            };
            menu_show.Click += (s, e) => SwitcherInstance.ShowTaskbar();
            menu_hide = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_hide)),
                Font = font
            };
            menu_hide.Click += (s, e) => SwitcherInstance.HideTaskbar();
            menu_exit = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_exit)),
                Font = font
            };
            menu_exit.Click += (s, e) =>
            {
                notify.Dispose();
                System.Windows.Application.Current.Shutdown();
            };

            contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };
            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                menu_about,
                menu_settings,
                new ToolStripSeparator(),
                menu_auto,
                menu_show,
                menu_hide,
                new ToolStripSeparator(),
                menu_exit
            });
            notify = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = Application.ProductName,
                Icon = Properties.Resources.logo_32,
                Visible = true
            };
            notify.MouseDoubleClick += (s, e) => SwitcherInstance.SwitchTaskbar();
            notify.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    //todo
                }
            };
        }
    }
}
