using System.Text;
using MedX.Service.Mappers;
using MedX.Service.Services;
using MedX.Data.Repositories;
using MedX.Data.IRepositories;
using MedX.Service.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MedX.Desktop.Extensions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAffairService, AffairService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<ICashDeskService, CashDeskService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped<IAffairItemService, AffairItemService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddHttpContextAccessor();
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}
