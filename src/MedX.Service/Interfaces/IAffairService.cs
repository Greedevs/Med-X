using MedX.Domain.Configurations;
using MedX.Service.DTOs.Administrators;
using MedX.Service.DTOs.Services;

namespace MedX.Service.Interfaces;

public interface IAffairService
{
    Task<AffairResultDto> AddAsync(AffairCreationDto dto);
    Task<AffairResultDto> UpdateAsync(AffairUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<AffairResultDto> GetAsync(long id);
    Task<IEnumerable<AffairResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}