using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Properties;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    internal class SystemTray : ApplicationContext
    {
        private readonly NotifyIcon notifyIcon;
        private readonly ContextMenuStrip contextMenuStrip;
        private readonly ToolStripMenuItem menu_settings;
        private readonly ToolStripMenuItem menu_auto;
        private readonly ToolStripMenuItem menu_exit;

        private readonly NotifierLauncher notifierLauncher = new NotifierLauncher();

        public SystemTray()
        {
            #region Initialization

            var resource = new ResourceCulture();
            var font = new Font("Segoe UI", 9F);
            menu_settings = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(menu_settings)),
                Font = font
            };
            menu_auto = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(menu_auto)),
                Font = font
            };
            menu_exit = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(menu_exit)),
                Font = font
            };
            contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };

            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                menu_settings,
                menu_auto,
                new ToolStripSeparator(),
                menu_exit
            });

            notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = @"SmartTaskbar v1.1.9",
                Icon = Environment.OSVersion.Version.Major.ToString() == "10" ? Resources.logo_32 : Resources.logo_blue_32,
                Visible = true
            };

            #endregion

            #region Load Event

            menu_settings.Click += (s, e) => SettingsForm.Instance.ChangeDisplayState();

            menu_exit.Click += (s, e) =>
            {
                notifierLauncher.Stop();
                Reset();
                notifyIcon.Dispose();
                SettingsForm.Instance.Dispose();
                Application.Exit();
            };

            notifyIcon.Click += (s, e) =>
            {


            };

            notifyIcon.MouseDoubleClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Left) return;

            };

            #endregion

            #region Load Settings


            #endregion
        }
    }
}