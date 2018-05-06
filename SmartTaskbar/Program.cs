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
            //https://stackoverflow.com/questions/1207105/restrict-multiple-instances-of-an-application
            using (Mutex mutex = new Mutex(true, "{214988ad-4d86-4bfa-943c-febeda556d46}", out bool createNew))
            {
                if (!createNew)
                    return;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new SystemTray();
                Application.Run();
            }
        }
    }
}
