using System;
using System.ComponentModel;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Core.AutoMode;
using SmartTaskbar.Core.UserConfig;

namespace SmartTaskbar
{
    internal class TaskbarController : ApplicationContext
    {
        private static int _count;
        private readonly Container _container = new Container();
        private readonly Timer _timer;
        private readonly IconTray _tray;
        private readonly IAutoMode _autoMode;

        public TaskbarController()
        {
            // Initialize the native method.
            InvokeMethods.Initialization();

            // Load AutoMode as fast as possible.
            switch (InvokeMethods.Settings.ModeType)
            {
                case AutoModeType.Disabled:
                    _autoMode = new DumbMode();
                    break;
                case AutoModeType.ForegroundMode:
                    _autoMode = new ForegroundMode();
                    break;
                case AutoModeType.ClassicAutoMode:
                case AutoModeType.ClassicAdaptiveMode:
                case AutoModeType.WhitelistMode:
                case AutoModeType.BlacklistMode:
                    _autoMode = new AutoMode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // timer is running on UI thread.
            _timer = new Timer(_container) {Interval = 375};
            // if AutoMode is Enabled, then run it every 375 milliseconds.
            _timer.Tick += Timer_Tick;
            _timer.Start();
            // Start Settings Tray
            _tray = new IconTray(_container);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ++_count;
            // Run AutoMode If Enable
            if (InvokeMethods.Settings.AutoHide) _autoMode.Run();

            // Reset AutoMode each 10+ second.
            if (_count % 27 == 0) _autoMode.Ready();

            // Update Taskbar each 10 minute.
            if (_count % 1600 == 0)
            {
                InvokeMethods.UpdateCache();
                _count = 0;
            }
        }
    }
}