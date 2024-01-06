using MedX.Service.DTOs.ServiceItems;

namespace MedX.Desktop.Services;

public interface IAffairItemsApiClient
{
    [Post("create")]
    Task<AffairItemResultDto> AddAsync(AffairItemCreationDto dto);

    [Put("update")]
    Task<AffairItemResultDto> UpdateAsync(AffairItemUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<AffairItemResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<AffairItemResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}