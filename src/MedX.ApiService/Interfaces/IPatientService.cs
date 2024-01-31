using MedX.ApiService.Models.Patients;

namespace MedX.ApiService.Interfaces;

public interface IPatientService
{
    Task<Response<PatientResultDto>> AddAsync(PatientCreationDto dto);
    Task<Response<PatientResultDto>> UpdateAsync(PatientUpdateDto dto);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<PatientResultDto>> GetAsync(long id);
    Task<Response<IEnumerable<PatientResultDto>>> GetAllAsync(PaginationParams @params, string search = null!);
}
