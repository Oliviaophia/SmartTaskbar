using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Views;
using Timer = System.Timers.Timer;

namespace SmartTaskbar
{
    internal class MainController : ApplicationContext
    {
        private readonly Container _container = new Container();
        private readonly Tray _tray;
        private readonly Timer _timer = new Timer(375);

        public MainController()
        {
            _tray = new Tray(_container);
            _timer.Elapsed += (sender, args) =>
            {
                InvokeMethods.AutoModeRun();
            };
            _timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _container?.Dispose();

            base.Dispose(disposing);
        }
    }
}