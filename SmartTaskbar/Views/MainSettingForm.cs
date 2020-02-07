using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Model;

namespace SmartTaskbar.Views
{
    public partial class MainSettingForm : Form
    {
        private readonly CoreInvoker _coreInvoker;
        private readonly IContainer _container;

        public MainSettingForm(IContainer container, CoreInvoker coreInvoker)
        {
            InitializeComponent();
        }
    }
}