using System;
using System.Threading;
using System.Windows.Forms;

namespace SmartTaskbar
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Use a mutex to ensure single instance
            using (new Mutex(true, "{959d3545-aa5c-42a8-a327-6e2c079daa94}", out bool createNew))
            {
                if (!createNew)
                    return;
                Application.AddMessageFilter(new MsgFilter());
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Start a tray instead of a WinForm to reduce memory usage
                Application.Run(new SystemTray());
            }
        }
    }
}
