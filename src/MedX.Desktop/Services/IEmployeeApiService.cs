using MedX.Desktop.Models;
using MedX.Service.DTOs.Employees;

namespace MedX.Desktop.Services;

public interface IEmployeeApiService
{
    [Post("/employees/create")]
    Task<Response<EmployeeResultDto>> AddAsync(EmployeeCreationDto dto);

    [Put("/employees/update")]
    Task<Response<EmployeeResultDto>> UpdateAsync(EmployeeUpdateDto dto);

    [Delete("/employees/delete/{id}")]
    Task<Response<bool>> DeleteAsync([AliasAs("id")] long id);

    [Get("/employees/get/{id}")]
    Task<Response<EmployeeResultDto>> GetAsync([AliasAs("id")] long id);

    [Get("/employees/get-all")]
    Task<Response<IEnumerable<EmployeeResultDto>>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}
