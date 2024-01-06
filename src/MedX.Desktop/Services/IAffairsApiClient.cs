using MedX.Service.DTOs.Services;

namespace MedX.Desktop.Services;

public interface IAffairsApiClient
{
    [Post("create")]
    Task<AffairResultDto> AddAsync(AffairCreationDto dto);

    [Put("update")]
    Task<AffairResultDto> UpdateAsync(AffairUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<AffairResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<AffairResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}