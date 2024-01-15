using MedX.ApiService.Models.Employees;

namespace MedX.ApiService.Interfaces;

public interface IEmployeeService
{
    Task<Response<EmployeeResultDto>> AddAsync(EmployeeCreationDto dto);
    Task<Response<EmployeeResultDto>> UpdateAsync(EmployeeUpdateDto dto);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<EmployeeResultDto>> GetAsync(long id);
    Task<Response<IEnumerable<EmployeeResultDto>>> GetAllAsync(PaginationParams @params, string search = null!);
}
