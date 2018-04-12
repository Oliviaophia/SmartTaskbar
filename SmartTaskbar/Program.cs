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
            using (Mutex mutex = new Mutex(true, "{4921a9b3-aff7-49f3-b145-0b4cb2e40074}", out bool createNew))
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
