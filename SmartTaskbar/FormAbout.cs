using System.Diagnostics;
using System.Windows.Forms;

namespace SmartTaskbar
{
    public partial class FormAbout : Form
    {
        public FormAbout()
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
    }
}
