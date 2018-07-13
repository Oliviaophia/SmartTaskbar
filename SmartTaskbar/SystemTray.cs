using System.Windows.Forms;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem about, transparent, smallIcon, animation, auto_size, auto_display, exit;
        private TaskbarSwitcher switcher = new TaskbarSwitcher();
        private Timer timer = new Timer();

        public SystemTray()
        {
            bool isWin10 = System.Environment.OSVersion.Version.Major.ToString() == "10";
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
            if (isWin10)
            {
                transparent = new ToolStripMenuItem
                {
                    Text = resource.GetString(nameof(transparent)),
                    Font = font
                };
                contextMenuStrip.Items.AddRange(new ToolStripItem[]
                {
                    about,
                    smallIcon,
                    animation,
                    transparent,
                    new ToolStripSeparator(),
                    auto_display,
                    auto_size,
                    new ToolStripSeparator(),
                    exit
                });

                timer.Interval = 15;
                timer.Tick += (s, e) => Transparent();
            }
            else
            {
                contextMenuStrip.Items.AddRange(new ToolStripItem[]
                {
                    about,
                    smallIcon,
                    animation,
                    new ToolStripSeparator(),
                    auto_display,
                    auto_size,
                    new ToolStripSeparator(),
                    exit
                });
            }

            notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = "SmartTaskbar v1.1.8",
                Icon = isWin10 ? Properties.Resources.logo_32 : Properties.Resources.logo_blue_32,
                Visible = true
            };
            #endregion

            #region Load Event

            about.Click += (s, e) => System.Diagnostics.Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar/releases");

            Properties.Settings.Default.PropertyChanged += (s, e) => Properties.Settings.Default.Save();

            transparent.Click += (s, e) => transparent.Checked = timer.Enabled = Properties.Settings.Default.Transparent = !transparent.Checked;

            smallIcon.Click += (s, e) =>
            {
                if (smallIcon.Checked)
                {
                    Properties.Settings.Default.IconSize = 0;
                    smallIcon.Checked = false;
                }
                else
                {
                    Properties.Settings.Default.IconSize = 1;
                    smallIcon.Checked = true;
                }
                SetIconSize(Properties.Settings.Default.IconSize);
            };

            animation.Click += (s, e) => animation.Checked = ChangeTaskbarAnimation();

            auto_size.Click += (s, e) =>
            {
                if (auto_size.Checked)
                {
                    switcher.Stop();
                    Properties.Settings.Default.TaskbarState = (int)AutoModeType.none;
                    smallIcon.Enabled = true;
                    auto_size.Checked = false;
                }
                else
                {
                    switcher.Start(AutoModeType.size);
                    Properties.Settings.Default.TaskbarState = (int)AutoModeType.size;
                    auto_size.Checked = true;
                    auto_display.Checked = smallIcon.Enabled = false;
                }
            };

            auto_display.Click += (s, e) =>
            {
                if (auto_display.Checked)
                {
                    switcher.Stop();
                    Properties.Settings.Default.TaskbarState = (int)AutoModeType.none;
                    auto_display.Checked = false;
                }
                else
                {
                    switcher.Start(AutoModeType.display);
                    Properties.Settings.Default.TaskbarState = (int)AutoModeType.display;
                    auto_display.Checked = true;
                    auto_size.Checked = false;
                }
                smallIcon.Enabled = true;
            };

            exit.Click += (s, e) =>
            {
                switcher.Stop();
                switcher.Reset();
                notifyIcon.Dispose();
                Application.Exit();
            };

            if (isWin10)
            {
                notifyIcon.MouseClick += (s, e) =>
                {
                    if (e.Button != MouseButtons.Right)
                        return;

                    switcher.Resume();

                    animation.Checked = GetTaskbarAnimation();

                    if (smallIcon.Enabled)
                        SetIconSize(Properties.Settings.Default.IconSize);

                    UpdataTaskbarHandle();
                };
            }
            else
            {
                notifyIcon.MouseClick += (s, e) =>
                {
                    if (e.Button != MouseButtons.Right)
                        return;

                    switcher.Resume();

                    animation.Checked = GetTaskbarAnimation();

                    if (smallIcon.Enabled)
                        SetIconSize(Properties.Settings.Default.IconSize);
                };
            }


            notifyIcon.MouseDoubleClick += (s, e) =>
            {
                smallIcon.Enabled = true;
                switcher.ChangeState();
                SetIconSize(Properties.Settings.Default.IconSize);
                auto_size.Checked = auto_display.Checked = false;
            };

            #endregion

            #region Load Settings

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

            transparent.Checked = timer.Enabled = Properties.Settings.Default.Transparent;
            #endregion
        }
    }
}