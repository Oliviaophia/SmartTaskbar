using System;
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

        public static bool IsWin10 { get; } = Environment.OSVersion.Version.Major.ToString() == "10";

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
            menu_auto.Click += (s, e) => 
            {
                if (menu_auto.Checked)
                    return;
                SwitcherInstance.DefaultMode(IsWin10);
                RadioChecked(ref menu_auto);
            };
            menu_show = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_show)),
                Font = font
            };
            menu_show.Click += (s, e) => 
            {
                if (menu_show.Checked)
                    return;
                SwitcherInstance.ShowTaskbar();
                RadioChecked(ref menu_show);
            };
            menu_hide = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_hide)),
                Font = font
            };
            menu_hide.Click += (s, e) => 
            {
                if(menu_hide.Checked)
                    return;
                SwitcherInstance.HideTaskbar();
                RadioChecked(ref menu_hide);
            };
            menu_exit = new ToolStripMenuItem
            {
                Text = CultureInstance.GetString(nameof(menu_exit)),
                Font = font
            };
            menu_exit.Click += (s, e) =>
            {
                notify.Dispose();
                SwitcherInstance.CloseSwitcher();
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
                    switch (Properties.Settings.Default.DefaultMode)
                    {
                        case nameof(menu_hide):
                            if (!SwitcherInstance.IsTaskbarAutoHide)
                                RadioChecked(ref menu_show);
                            break;
                        case nameof(menu_show):
                            if (SwitcherInstance.IsTaskbarAutoHide)
                                RadioChecked(ref menu_hide);
                            break;
                        default:
                            break;
                    }
                }
            };

            #region LoadSettings

            //LoadDefaultTaskbarMode:
            switch (Properties.Settings.Default.DefaultMode)
            {
                case nameof(menu_hide):
                    SwitcherInstance.HideTaskbar();
                    break;
                case nameof(menu_show):
                    SwitcherInstance.ShowTaskbar();
                    break;
                default:
                    SwitcherInstance.DefaultMode(IsWin10);
                    break;
            }
            #endregion
        }

        private void RadioChecked(ref ToolStripMenuItem tool)
        {
            menu_auto.Checked = menu_hide.Checked = menu_show.Checked = false;
            Properties.Settings.Default.DefaultMode = tool.Name;
            Properties.Settings.Default.Save();
            tool.Checked = true;
        }
    }
}
