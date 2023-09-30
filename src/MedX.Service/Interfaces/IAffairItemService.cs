using MedX.Domain.Configurations;
using MedX.Service.DTOs.ServiceItems;

namespace MedX.Service.Interfaces;

public interface IAffairItemService
{
    Task<ServiceItemResultDto> AddAsync(ServiceItemCreationDto dto);
    Task<ServiceItemResultDto> UpdateAsync(ServiceItemUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ServiceItemResultDto> GetAsync(long id);
    Task<IEnumerable<ServiceItemResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}
