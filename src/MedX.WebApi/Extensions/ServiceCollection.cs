using MedX.Service.Mappers;
using MedX.Service.Services;
using MedX.Data.Repositories;
using MedX.Data.IRepositories;
using MedX.Service.Interfaces;

namespace MedX.WebApi.Extensions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}