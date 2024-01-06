using MedX.Service.DTOs.Patients;

namespace MedX.Desktop.Services;

public interface IPatientsApiClient
{
    [Post("create")]
    Task<PatientResultDto> AddAsync(PatientCreationDto dto);

    [Put("update")]
    Task<PatientResultDto> UpdateAsync(PatientUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<PatientResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<PatientResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}