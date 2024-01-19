using Microsoft.Extensions.DependencyInjection;
using MedX.ApiService.Services; // Assuming this namespace contains your service interfaces and implementations
using MedX.ApiService;

namespace MedX.Desktop
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; } = default!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure services
            ConfigureServices();

            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddHttpClient();
            services.AddApiServices();
            services.AddTransient<MainWindow>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
