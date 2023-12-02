using MedX.Domain.Configurations;
using MedX.Service.DTOs.Doctors;

namespace MedX.Service.Interfaces;

public interface IDoctorService
{
    Task<EmployeeResultDto> AddAsync(EmployeeCreationDto dto);
    Task<EmployeeResultDto> UpdateAsync(EmployeeUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<EmployeeResultDto> GetAsync(long id);
    Task<IEnumerable<EmployeeResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}
