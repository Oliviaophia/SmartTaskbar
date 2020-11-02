using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Models;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MainController : ApplicationContext
    {
        private readonly Container _container = new Container();
        private readonly CoreInvoker _coreInvoker = new CoreInvoker();
        private readonly MainNotifyIcon _tray;

        public MainController()
            => _tray = new MainNotifyIcon(_container, _coreInvoker);

        public void ShowSettingWindow()
        {
            // todo
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
