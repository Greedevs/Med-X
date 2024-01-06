using MedX.Service.DTOs.Treatments;

namespace MedX.Desktop.Services;

public interface ITreatmentsApiClient
{
    [Post("create")]
    Task<TreatmentResultDto> AddAsync(TreatmentCreationDto dto);

    [Put("update")]
    Task<TreatmentResultDto> UpdateAsync(TreatmentUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<TreatmentResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<TreatmentResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}