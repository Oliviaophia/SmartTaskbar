using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskbar.Engines;
using SmartTaskbar.Models.Interfaces;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.UI.Languages;
using SmartTaskbar.UI.Views;

namespace SmartTaskbar.UI
{
    internal static class Program
    {
        private static ServiceProvider _serviceProvider;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static async Task Main()
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

                    await DependencyInjection();

                    Application.Run(_serviceProvider.GetService<MainNotifyIcon>()!);
                }
            }
        }

        private static async Task DependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            await _serviceProvider.GetService<UserConfigEngine>()!.Initializer();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // todo add Services;
            serviceCollection.AddSingleton<IContainer, Container>();
            serviceCollection.AddSingleton<MainNotifyIcon>();
            serviceCollection.AddSingleton<CultureResource>();
            serviceCollection.AddSingleton<IUserConfigService, UserConfigService>();
            serviceCollection.AddSingleton<UserConfigEngine>();
        }
    }
}
