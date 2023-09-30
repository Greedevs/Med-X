using MedX.Domain.Configurations;
using MedX.Service.DTOs.CashDesks;

namespace MedX.Service.Interfaces;

public interface ICashDeskService
{
    Task<CashDeskResultDto> AddAsync(CashDeskCreationDto dto);
    Task<CashDeskResultDto> UpdateAsync(CashDeskUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<CashDeskResultDto> GetAsync(long id);
    Task<IEnumerable<CashDeskResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}
