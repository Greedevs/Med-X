using MedX.Domain.Configurations;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.Interfaces;

public interface IPatientService
{
    Task<PatientResultDto> AddAsync(PatientCreationDto dto);
    Task<PatientResultDto> UpdateAsync(PatientUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<PatientResultDto> GetAsync(long id);
    Task<IEnumerable<PatientResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}