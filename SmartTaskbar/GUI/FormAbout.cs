using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartTaskbar
{
    public partial class FormAbout : Form
    {
        private static readonly Lazy<FormAbout> lazy = new Lazy<FormAbout>(() => new FormAbout());

        public static FormAbout FormInstance => lazy.Value;

        private FormAbout()
        {
            InitializeComponent();
            linkWeb.Click += (s, e) => Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar");
            linkRelease.Click += (s, e) => Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar/releases");
            FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    Hide();
                    e.Cancel = true;
                }
            };
        }

        public void Switch()
        {
            if (Visible)
                Hide();
            else
                Show();
        }
    }
}
