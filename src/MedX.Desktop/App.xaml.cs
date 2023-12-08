using System.IO;
using System.Windows;
using MedX.Data.Contexts;
using MedX.Service.Helpers;
using MedX.Service.Mappers;
using MedX.Service.Services;
using MedX.Data.Repositories;
using MedX.Service.Interfaces;
using MedX.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MedX.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IServiceProvider serviceProvider;
    public static IServiceProvider ServiceProvider => serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Servicelarni qo'llab-quvvatlash uchun ServiceProvider ni o'rnatamiz
        // ConfigureServices();

        base.OnStartup(e);

        // Asosiy proyektning boshqa qismlari
        // var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
    }

    private void ConfigureServices()
    {
        PathHelper.WebRootPath = "../../../Assets";
        //C:\Users\muqim\source\repos\Med-X\src\MedX.Desktop\Assets\Images\b02b7de2c33648998eed4ac056d63cbf.png
        string sp = PathHelper.WebRootPath;
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql("host=localhost:5432;uid=postgres;password=root;database=MedXDb;"));

        services.AddHttpContextAccessor();
        services.AddTransient<MainWindow>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAffairService, AffairService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<ICashDeskService, CashDeskService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped<IAffairItemService, AffairItemService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        serviceProvider = services.BuildServiceProvider();
    }
}

