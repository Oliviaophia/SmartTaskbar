using System.Threading;
using System.Windows;

namespace SmartTaskbar
{
    public partial class App : Application
    {
        private static Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            mutex = new Mutex(true, "{5550298e-983e-4a85-bc24-cb08e2fe90e5}", out bool createdNew);
            if (!createdNew)
                Current.Shutdown();
            new SystemTray();
            base.OnStartup(e);
        }
    }
}
