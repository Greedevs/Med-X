using MedX.Domain.Configurations;
using MedX.Service.DTOs.Administrators;
using MedX.Service.DTOs.Services;

namespace MedX.Service.Interfaces;

public interface IAffairService
{
    Task<ServiceResultDto> AddAsync(ServiceCreationDto dto);
    Task<ServiceResultDto> UpdateAsync(ServiceUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ServiceResultDto> GetAsync(long id);
    Task<IEnumerable<ServiceResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}