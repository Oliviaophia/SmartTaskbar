using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Text = resource.GetString("auto")
            };
            auto.Click += Auto_Click;
            show = new ToolStripMenuItem
            {
                Text = resource.GetString("show")
            };
            show.Click += Show_Click;
            hide = new ToolStripMenuItem
            {
                Text = resource.GetString("hide")
            };
            hide.Click += Hide_Click;
            exit = new ToolStripMenuItem
            {
                Text = resource.GetString("exit")
            };
            exit.Click += (s, e) => Application.Exit();
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
            switch (Settings.Default.TaskbarState)
            {
                case 0:
                    auto.Checked = true;
                    break;
                case 1:
                    hide.Checked = true;
                    break;
                default:
                    show.Checked = true;
                    break;
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            switch (Settings.Default.TaskbarState)
            {
                case 0:
                    switcher.Resume();
                    break;
                case 1:
                    if (switcher.IsHide() == false)
                    {
                        RadioChecked(ref show);
                        Settings.Default.TaskbarState = 2;
                        Settings.Default.Save();
                    }
                    break;
                default:
                    if (switcher.IsHide())
                    {
                        RadioChecked(ref hide);
                        Settings.Default.TaskbarState = 1;
                        Settings.Default.Save();
                    }
                    break;
            }
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            if (hide.Checked)
                return;
            switcher.Hide();
            RadioChecked(ref hide);
            Settings.Default.TaskbarState = 1;
            Settings.Default.Save();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (show.Checked)
                return;
            switcher.Show();
            RadioChecked(ref show);
            Settings.Default.TaskbarState = 2;
            Settings.Default.Save();
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            if (auto.Checked)
                return;
            switcher.Start();
            RadioChecked(ref auto);
            Settings.Default.TaskbarState = 0;
            Settings.Default.Save();
        }

        private void RadioChecked(ref ToolStripMenuItem tool)
        {
            auto.Checked = show.Checked = hide.Checked = false;
            tool.Checked = true;
        }

        ~SystemTray()
        {
            notifyIcon.Dispose();
        }
    }
}
