using MedX.Domain.Configurations;
using MedX.Service.DTOs.Treatments;

namespace MedX.Service.Interfaces;

public interface ITreatmentService
{
    Task<TreatmentResultDto> AddAsync(TreatmentCreationDto dto);
    Task<TreatmentResultDto> UpdateAsync(TreatmentUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<TreatmentResultDto> GetAsync(long id);
    Task<IEnumerable<TreatmentResultDto>> GetAllAsync(PaginationParams @params, string search = null);
    Task<IEnumerable<TreatmentResultDto>> GetAllByPatientIdAsync(long patientId);
    Task<IEnumerable<TreatmentResultDto>> GetAllByDoctorIdAsync(long doctorId, PaginationParams @params, string search = null);
    Task<IEnumerable<TreatmentResultDto>> GetAllByRoomIdAsync(long roomId);
}
