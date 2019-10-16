using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Model;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MainController : ApplicationContext
    {
        private readonly Container _container = new Container();
        private readonly CoreInvoker _coreInvoker = new CoreInvoker();
        private readonly Tray _tray;

        public MainController()
        {
            _tray = new Tray(_container, _coreInvoker);
        }

        public void ShowSettingForm()
        {
            _tray.ShowSettingForm();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tray?.Dispose();
                _container?.Dispose();
                _coreInvoker?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}