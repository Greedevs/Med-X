using System.Windows;
using MedX.Service.Services;
using MedX.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MedX.Service.DTOs.Doctors;
using MedX.Service.Mappers;
using MedX.Data.IRepositories;
using MedX.Data.Repositories;
using MedX.Data.Contexts;
using System.Configuration;
using Microsoft.EntityFrameworkCore;


namespace MedX.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IServiceProvider _serviceProvider;

    public static IServiceProvider ServiceProvider => _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Servicelarni qo'llab-quvvatlash uchun ServiceProvider ni o'rnatamiz
        ConfigureServices();

        base.OnStartup(e);

        // Asosiy proyektning boshqa qismlari
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql("host=localhost:5432;uid=postgres;password=root;database=MedXDb;"));

        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(MappingProfile));
        
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAffairItemService, AffairItemService>();
        services.AddScoped<IAffairService, AffairService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICashDeskService, CashDeskService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<MainWindow>();
        _serviceProvider = services.BuildServiceProvider();
    }
}

