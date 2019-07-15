using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MainController : ApplicationContext
    {
        private readonly Container _container = new Container();
        private readonly Tray _tray;

        public MainController()
        {
            _tray = new Tray(_container);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _container?.Dispose();

            base.Dispose(disposing);
        }
    }
}