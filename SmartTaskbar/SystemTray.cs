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

        public SystemTray()
        {
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
                Text = "SmartTaskbar+",
                Visible = true
            };
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            RadioChecked(hide);
        }

        private void Show_Click(object sender, EventArgs e)
        {
            RadioChecked(show);
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            RadioChecked(auto);
        }

        private void RadioChecked(ToolStripMenuItem tool)
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
