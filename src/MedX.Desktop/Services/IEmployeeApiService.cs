using MedX.Desktop.Models;
using MedX.Desktop.Models.Employees;

namespace MedX.Desktop.Services;

public interface IEmployeeApiService
{
    [Multipart]
    [Post("/api/employees/create")]
    Task<Response<EmployeeResultDto>> AddAsync(EmployeeCreationDto dto);

    [Multipart]
    [Put("/api/employees/update")]
    Task<Response<EmployeeResultDto>> UpdateAsync(EmployeeUpdateDto dto);

    [Delete("/api/employees/delete/{id}")]
    Task<Response<bool>> DeleteAsync([AliasAs("id")] long id);

    [Get("/api/employees/get/{id}")]
    Task<Response<EmployeeResultDto>> GetAsync([AliasAs("id")] long id);

    [Get("/api/employees/get-all")]
    Task<Response<IEnumerable<EmployeeResultDto>>> GetAllAsync([Query] PaginationParams @params = default!, [Query] string? search = default!);
}
