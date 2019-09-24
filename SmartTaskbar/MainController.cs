using System;
using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MainController : ApplicationContext
    {
        private readonly Container _container = new Container();
        private readonly AutoModeController _autoModeController = new AutoModeController();
        private readonly Tray _tray;

        public MainController()
        {
            _tray = new Tray(_container, _autoModeController);
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
                _autoModeController?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}