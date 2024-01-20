using MedX.ApiService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedX.ApiService;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
        {
            client.BaseAddress = new Uri($"{HttpConstant.BaseLink}api/Employees/");
        });

        return services;
    }
}
