using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTaskbar.UI.Views
{
    public partial class MainSettingForm : Form
    {
        public MainSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            base.OnFormClosing(e);
        }
    }
}
