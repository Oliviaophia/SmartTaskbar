using System;
using System.Threading;
using System.Windows.Forms;

namespace SmartTaskbar
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            //Use a mutex to ensure single instance
            using (new Mutex(true, "{959d3545-aa5c-42a8-a327-6e2c079daa94}", out var createNew))
            {
                if (!createNew)
                    return;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Start a tray instead of a WinForm to reduce memory usage
                Application.Run(new SystemTray());
            }
        }
    }
}
