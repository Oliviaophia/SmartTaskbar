using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTaskbar
{
    public partial class SettingsForm : Form
    {
        public static SettingsForm Instance => instance.Value;

        private static readonly Lazy<SettingsForm> instance = new Lazy<SettingsForm>(() => new SettingsForm());

        private SettingsForm()
        {
            InitializeComponent();
            FormClosing += (s, e) =>
            {
                Hide();
                e.Cancel = true;
            };
        }

        public void ChangeDisplayState() => Visible = !Visible;

    }
}
