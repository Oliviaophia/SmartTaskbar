using Microsoft.Extensions.DependencyInjection;

namespace SmartTaskbar;

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
    private static void Main()
    {
        // Use a mutex to ensure single instance
        using (new Mutex(true, "{959d3545-aa5c-42a8-a327-6e2c079daa94}", out var createNew))
        {
            if (!createNew) return;

            ApplicationConfiguration.Initialize();
            // Start a tray instead of a WinForm to reduce memory usage
            Application.Run(ServiceProvider.GetService<SystemTray>()!);
        }
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<SystemTray>();
    }
}
