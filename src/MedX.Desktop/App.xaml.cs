using MedX.ApiService.Services; // Assuming this namespace contains your service interfaces and implementations
using MedX.ApiService;
using Microsoft.Extensions.DependencyInjection;

namespace MedX.Desktop;

public partial class App : Application
{
    private readonly IServiceProvider serviceProvider;
    private readonly IServiceCollection services;
    public App()
    {
        this.services = new ServiceCollection();
        this.serviceProvider = this.services.BuildServiceProvider();
        ConfigureService(services);
    }

    private void ConfigureService(IServiceCollection services)
    {
        services.AddApiServices();
        services.AddSingleton<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
