using MedX.Service.DTOs.CashDesks;

namespace MedX.Desktop.Services;

public interface ICashDesksApiClient
{
    [Post("create")]
    Task<CashDeskResultDto> AddAsync(CashDeskCreationDto dto);

    [Put("update")]
    Task<CashDeskResultDto> UpdateAsync(CashDeskUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<CashDeskResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<CashDeskResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}