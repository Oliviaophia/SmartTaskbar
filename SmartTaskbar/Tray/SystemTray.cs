using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem about, animation, auto, show, hide, exit;
        private TaskbarSwitcher switcher;

        public SystemTray()
        {
            ResourceCulture resource = new ResourceCulture();
            System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 9F);
            about = new ToolStripMenuItem
            {
                Text = resource.GetString("about"),
                Font = font
            };
            //about.Click += (s, e) => Process.Start("https://github.com/ChanpleCai/SmartTaskbar");
            animation = new ToolStripMenuItem
            {
                Text = resource.GetString("animation"),
                Font = font
            };
            animation.Click += Animation_Click;
            auto = new ToolStripMenuItem
            {
                Text = resource.GetString("auto"),
                Name = "auto",
                Font = font
            };
            auto.Click += Auto_Click;
            show = new ToolStripMenuItem
            {
                Text = resource.GetString("show"),
                Name = "show",
                Font = font
            };
            show.Click += Show_Click;
            hide = new ToolStripMenuItem
            {
                Text = resource.GetString("hide"),
                Name = "hide",
                Font = font
            };
            hide.Click += Hide_Click;
            exit = new ToolStripMenuItem
            {
                Text = resource.GetString("exit"),
                Font = font
            };
            exit.Click += Exit_Click;
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
                Text = Application.ProductName,
                Icon = Resource_Icon.logo_32,
                Visible = true
            };
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            if (Settings.Default.SwitcherVersion == 0)
            {
                Settings.Default.SwitcherVersion = Environment.OSVersion.Version.Major.ToString() == "10" ? 1 : 3;
                if (Environment.Is64BitOperatingSystem)
                    ++Settings.Default.SwitcherVersion;
                Settings.Default.Save();
                notifyIcon.BalloonTipTitle = Application.ProductName;
                notifyIcon.BalloonTipText = resource.GetString("firstrun");
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.ShowBalloonTip(5);
            }
            switcher = new TaskbarSwitcher();
            switch (Settings.Default.TaskbarState)
            {
                case "auto":
                    switcher.Start();
                    auto.Checked = true;
                    break;
                case "hide":
                    hide.Checked = true;
                    break;
                default:
                    show.Checked = true;
                    break;
            }
        }

        private void Animation_Click(object sender, EventArgs e) => animation.Checked = switcher.AnimationSwitcher();

        private void Exit_Click(object sender, EventArgs e)
        {
            switcher.Stop();
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (switcher.IsHide())
            {
                switcher.Show();
                RadioChecked(ref show);
            }
            else
            {
                switcher.Hide();
                RadioChecked(ref hide);
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            switch (Settings.Default.TaskbarState)
            {
                case "auto":
                    switcher.Resume();
                    break;
                case "hide":
                    if (switcher.IsHide() == false)
                        RadioChecked(ref show);
                    break;
                default:
                    if (switcher.IsHide())
                        RadioChecked(ref hide);
                    break;
            }
            animation.Checked = switcher.IsAnimationEnable();
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            if (hide.Checked)
                return;
            switcher.Hide();
            RadioChecked(ref hide);
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (show.Checked)
                return;
            switcher.Show();
            RadioChecked(ref show);
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            if (auto.Checked)
                return;
            switcher.Start();
            RadioChecked(ref auto);
        }

        private void RadioChecked(ref ToolStripMenuItem tool)
        {
            auto.Checked = show.Checked = hide.Checked = false;
            Settings.Default.TaskbarState = tool.Name;
            Settings.Default.Save();
            tool.Checked = true;
        }
    }
}
