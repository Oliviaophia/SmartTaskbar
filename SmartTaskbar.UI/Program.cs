using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskbar.Engines;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models.Interfaces;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.UI.Languages;
using SmartTaskbar.UI.Views;

namespace SmartTaskbar.UI
{
    internal static class Program
    {
        private static readonly ServiceProvider ServiceProvider;

        static Program()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Use a mutex to ensure single instance
            using (new Mutex(true, "{959d3545-aa5c-42a8-a327-6e2c079daa94}", out var createNew))
            {
                if (createNew)
                {
                    // Start a tray instead of a WinForm to reduce memory usage
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(ServiceProvider.GetService<MainNotifyIcon>()
                                    ?? throw new NullReferenceException("The System Tray Icon failed to load."));
                }
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<CultureResource>();
            serviceCollection.AddSingleton<IUserConfigService, UserConfigService>();
            serviceCollection.AddSingleton<UserConfigEngine<MainViewModel>>();
            serviceCollection.AddSingleton<IUserConfigEngine>(x => x.GetService<UserConfigEngine<MainViewModel>>()
                                                                   ?? throw new NullReferenceException(
                                                                       "The user settings failed to load."));
            serviceCollection.AddSingleton<AutoModeWorker>();
            serviceCollection.AddSingleton<TimeEngine>();
            serviceCollection.AddSingleton<MainNotifyIcon>();
        }
    }
}
