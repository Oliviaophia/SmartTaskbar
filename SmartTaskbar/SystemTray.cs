using System.Windows.Forms;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem about, smallIcon, animation, auto_size, auto_display, exit;
        private TaskbarSwitcher switcher = new TaskbarSwitcher();

        public SystemTray()
        {
            #region Initialization
            ResourceCulture resource = new ResourceCulture();
            System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 9F);
            about = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(about)),
                Font = font
            };
            smallIcon = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(smallIcon)),
                Font = font
            };
            animation = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(animation)),
                Font = font
            };
            auto_size = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(auto_size)),
                Font = font
            };
            auto_display = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(auto_display)),
                Font = font
            };
            exit = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(exit)),
                Font = font
            };
            contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };
            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                about,
                smallIcon,
                animation,
                new ToolStripSeparator(),
                auto_size,
                auto_display,
                new ToolStripSeparator(),
                exit
            });
            notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = "SmartTaskbar",
                Icon = System.Environment.OSVersion.Version.Major.ToString() == "10" ? Properties.Resources.logo_32 : Properties.Resources.logo_blue_32,
                Visible = true
            };
            #endregion

            #region Load Event

            about.Click += (s, e) => System.Diagnostics.Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar/releases");

            smallIcon.Click += (s, e) =>
            {
                if (smallIcon.Checked)
                {
                    Properties.Settings.Default.IconSize = 0;
                    Properties.Settings.Default.Save();
                    switcher.SetSize();
                    smallIcon.Checked = false;
                    return;
                }
                Properties.Settings.Default.IconSize = 1;
                Properties.Settings.Default.Save();
                switcher.SetSize();
                smallIcon.Checked = true;
            };

            animation.Click += (s, e) => animation.Checked = switcher.AnimationSwitcher();

            auto_size.Click += (s, e) =>
            {
                if (auto_size.Checked)
                {
                    auto_size.Checked = false;
                    Properties.Settings.Default.TaskbarState = (int)AutoModeType.none;
                    Properties.Settings.Default.Save();
                    switcher.Stop();
                    smallIcon.Enabled = true;
                    return;
                }
                switcher.Start(AutoModeType.size);
                auto_size.Checked = true;
                auto_display.Checked = smallIcon.Enabled = false;
                Properties.Settings.Default.TaskbarState = (int)AutoModeType.size;
                Properties.Settings.Default.Save();
            };

            auto_display.Click += (s, e) =>
            {
                smallIcon.Enabled = true;
                if (auto_display.Checked)
                {
                    auto_display.Checked = false;
                    Properties.Settings.Default.TaskbarState = (int)AutoModeType.none;
                    Properties.Settings.Default.Save();
                    switcher.Stop();
                    return;
                }
                switcher.Start(AutoModeType.display);
                auto_display.Checked = true;
                auto_size.Checked = false;
                Properties.Settings.Default.TaskbarState = (int)AutoModeType.display;
                Properties.Settings.Default.Save();
            };

            exit.Click += (s, e) =>
            {
                switcher.Stop();
                switcher.Reset();
                notifyIcon.Dispose();
                Application.Exit();
            };

            notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Right)
                    return;

                switcher.Resume();
                animation.Checked = switcher.IsAnimationEnable();
                if (smallIcon.Enabled)
                    switcher.SetSize();
            };

            notifyIcon.MouseDoubleClick += (s, e) =>
            {
                smallIcon.Enabled = true;
                switcher.ChangeState();
                switcher.SetSize();
                auto_size.Checked = auto_display.Checked = false;
            };

            #endregion

            #region Load Check State

            switch ((AutoModeType)Properties.Settings.Default.TaskbarState)
            {
                case AutoModeType.display:
                    auto_display.Checked = true;
                    break;
                case AutoModeType.size:
                    auto_size.Checked = true;
                    smallIcon.Enabled = false;
                    break;
            }

            if (Properties.Settings.Default.IconSize == 1)
                smallIcon.Checked = true;

            #endregion
        }
    }
}