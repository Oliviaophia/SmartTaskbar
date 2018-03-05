using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartTaskbar
{
    class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem about, auto, show, hide, exit;
        private TaskbarSwitcher switcher;

        public SystemTray()
        {
            switcher = new TaskbarSwitcher();
            ResourceCulture resource = new ResourceCulture();
            about = new ToolStripMenuItem
            {
                Text = resource.GetString("about")
            };
            about.Click += (s, e) => Process.Start("https://github.com/ChanpleCai/SmartTaskbar");
            auto = new ToolStripMenuItem
            {
                Text = resource.GetString("auto"),
                Name = "auto"
            };
            auto.Click += Auto_Click;
            show = new ToolStripMenuItem
            {
                Text = resource.GetString("show"),
                Name = "show"
            };
            show.Click += Show_Click;
            hide = new ToolStripMenuItem
            {
                Text = resource.GetString("hide"),
                Name = "hide"
            };
            hide.Click += Hide_Click;
            exit = new ToolStripMenuItem
            {
                Text = resource.GetString("exit")
            };
            exit.Click += Exit_Click;
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                about,
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
                Icon = Resource_Icon.logo_32,
                Text = "SmartTaskbar",
                Visible = true
            };
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
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
