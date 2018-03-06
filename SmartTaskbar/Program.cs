using System;
using System.Threading;
using System.Windows.Forms;

namespace SmartTaskbar
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(true, "{741a1a00-96e2-451a-bd3c-1864fb1fbc66}", out bool createNew))
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
