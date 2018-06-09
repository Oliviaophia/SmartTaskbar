using System.Windows.Forms;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem about, animation, auto, show, hide, exit;
        private Switcher.TaskbarSwitcher switcher = new Switcher.TaskbarSwitcher();

        public SystemTray()
        {
            #region Load Auto-Mode
            
            if (Properties.Settings.Default.TaskbarState.Equals(nameof(auto)))
                switcher.Start();

            #endregion

            #region Initialization
            ResourceCulture resource = new ResourceCulture();
            System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 9F);
            about = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(about)),
                Font = font
            };
            animation = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(animation)),
                Font = font
            };
            auto = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(auto)),
                Name = nameof(auto),
                Font = font
            };
            show = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(show)),
                Name = nameof(show),
                Font = font
            };
            hide = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(hide)),
                Name = nameof(hide),
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
                animation,
                new ToolStripSeparator(),
                auto,
                show,
                hide,
                new ToolStripSeparator(),
                exit
            });
            notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = "SmartTaskbar v1.1.6",
                Icon = Properties.Resources.logo_32,
                Visible = true
            };
            #endregion

            #region Load Event

            about.Click += (s, e) => System.Diagnostics.Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar/releases");

            animation.Click += (s, e) => animation.Checked = switcher.AnimationSwitcher();

            auto.Click += (s, e) =>
            {
                if (auto.Checked)
                    return;
                switcher.Start();
                RadioChecked(ref auto);
            };

            show.Click += (s, e) =>
            {
                if (show.Checked)
                    return;
                switcher.Show();
                RadioChecked(ref show);
            };

            hide.Click += (s, e) =>
            {
                if (hide.Checked)
                    return;
                switcher.Hide();
                RadioChecked(ref hide);
            };

            exit.Click += (s, e) =>
            {
                switcher.Stop();
                notifyIcon.Dispose();
                Application.Exit();
            };

            notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Right)
                    return;

                switch (Properties.Settings.Default.TaskbarState)
                {
                    case "auto":
                        switcher.Resume();
                        break;
                    case "hide":
                        if (!switcher.IsHide())
                            RadioChecked(ref show);
                        break;
                    default:
                        if (switcher.IsHide())
                            RadioChecked(ref hide);
                        break;
                }
                animation.Checked = switcher.IsAnimationEnable();
            };

            notifyIcon.MouseDoubleClick += (s, e) =>
            {
                if (switcher.IsHide())
                {
                    switcher.Show();
                }
                else
                {
                    switcher.Hide();
                }
            };

            #endregion

            #region Load Check State

            switch (Properties.Settings.Default.TaskbarState)
            {
                case "auto":
                    auto.Checked = true;
                    break;
                case "hide":
                    hide.Checked = true;
                    break;
                default:
                    show.Checked = true;
                    break;
            }

            #endregion
        }

        private void RadioChecked(ref ToolStripMenuItem tool)
        {
            auto.Checked = show.Checked = hide.Checked = false;
            Properties.Settings.Default.TaskbarState = tool.Name;
            Properties.Settings.Default.Save();
            tool.Checked = true;
        }
    }
}