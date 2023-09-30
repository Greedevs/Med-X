using MedX.Domain.Configurations;
using MedX.Service.DTOs.ServiceItems;

namespace MedX.Service.Interfaces;

public interface IAffairItemService
{
    Task<AffairItemResultDto> AddAsync(AffairItemCreationDto dto);
    Task<AffairItemResultDto> UpdateAsync(AffairItemUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<AffairItemResultDto> GetAsync(long id);
    Task<IEnumerable<AffairItemResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}
