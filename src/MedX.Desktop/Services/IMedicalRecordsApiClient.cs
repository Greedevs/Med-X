using MedX.Service.DTOs.MedicalRecords;

namespace MedX.Desktop.Services;

public interface IMedicalRecordsApiClient
{
    [Post("create")]
    Task<MedicalRecordResultDto> AddAsync(MedicalRecordCreationDto dto);

    [Put("update")]
    Task<MedicalRecordResultDto> UpdateAsync(MedicalRecordUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<MedicalRecordResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<MedicalRecordResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}