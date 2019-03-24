using System;
using System.Threading.Tasks;
using SmartTaskbar.Core;
using SmartTaskbar.Core.AutoMode;

namespace AutoModeTest
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main()
        {
            IAutoMode autoMode = new ForegroundMode();
            autoMode.Ready();
            while (true)
            {
                await Task.Delay(500);
                InvokeMethods.UpdateCache();
                autoMode.Run();
            }
        }
    }
}