using MedX.Domain.Configurations;
using MedX.Service.DTOs.Administrators;

namespace MedX.Service.Interfaces;

public interface IAdminService
{
    Task<AdminResultDto> AddAsync(AdminCreationDto dto);
    Task<AdminResultDto> UpdateAsync(AdminUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<AdminResultDto> GetAsync(long id);
    Task<IEnumerable<AdminResultDto>> GetAllAsync(PaginationParams @params, string search = null);
    Task<IEnumerable<AdminResultDto>> SearchByQuery(string query);
}
