using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SmartTaskbar.Core;

namespace SmartTaskbar
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Use a mutex to ensure single instance
            using (new Mutex(true, "{959d3545-aa5c-42a8-a327-6e2c079daa94}", out var createNew))
            {
                if (createNew)
                {
                    // Start a tray instead of a WinForm to reduce memory usage
                    Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    var main = new MainController();
                    Application.AddMessageFilter(new MsgFilter(main));
                    Application.Run(main);
                }

                // Show the settings window if an instance already exists
                var process = Process.GetProcessesByName(Application.ProductName)
                    .FirstOrDefault(_ => _.Threads[0].Id != Process.GetCurrentProcess().Threads[0].Id);
                if (process is null) return;

                InvokeMethods.BringOutSettingsWindow(process.Threads[0].Id);
            }
        }
    }
}