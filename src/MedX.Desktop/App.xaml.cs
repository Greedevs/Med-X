using MedX.Data.Contexts;
using MedX.Data.IRepositories;
using MedX.Data.Repositories;
using MedX.Service.Interfaces;
using MedX.Service.Mappers;
using MedX.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MedX.Desktop;

public partial class App : Application
{
    private ServiceProvider serviceProvider;
    public App()
    {
        ServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        this.serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql("host = 52.221.226.79:5432; uid = postgres; password = root; database = MedXDb;"));

        services.AddSingleton<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}

