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
            using (Mutex mutex = new Mutex(true, "f6b3a3e6-625c-4bd1-bcdc-b750b1a865ce", out bool createNew))
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
