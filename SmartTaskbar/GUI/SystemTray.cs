using System;
using System.Windows.Forms;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem about, animation, auto, show, hide, exit;
        private TaskbarSwitcher switcher;
        private FormAbout form;

        public SystemTray()
        {
            ResourceCulture resource = new ResourceCulture();
            System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 9F);
            about = new ToolStripMenuItem
            {
                Text = resource.GetString("about"),
                Font = font
            };
            animation = new ToolStripMenuItem
            {
                Text = resource.GetString("animation"),
                Font = font
            };
            auto = new ToolStripMenuItem
            {
                Text = resource.GetString("auto"),
                Name = "auto",
                Font = font
            };
            show = new ToolStripMenuItem
            {
                Text = resource.GetString("show"),
                Name = "show",
                Font = font
            };
            hide = new ToolStripMenuItem
            {
                Text = resource.GetString("hide"),
                Name = "hide",
                Font = font
            };
            exit = new ToolStripMenuItem
            {
                Text = resource.GetString("exit"),
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
                Text = Application.ProductName,
                Icon = Resource_Icon.logo_32,
                Visible = true
            };
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

            about.Click += (s, e) =>
            {
                form = form ?? new FormAbout();
                form.Show();
            };

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

            notifyIcon.Click += (s, e) =>
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
            };

            notifyIcon.MouseDoubleClick += (s, e) =>
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
            };
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
