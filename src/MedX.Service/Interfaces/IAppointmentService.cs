using MedX.Domain.Configurations;
using MedX.Service.DTOs.Appointments;

namespace MedX.Service.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResultDto> AddAsync(AppointmentCreationDto dto);
    Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<AppointmentResultDto> GetAsync(long id);
    Task<IEnumerable<AppointmentResultDto>> GetAllAsync(PaginationParams @params, string search = null);
    Task<IEnumerable<AppointmentResultDto>> GetAllByDoctorIdAsync(long doctorId, PaginationParams @params, string search = null);
    Task<IEnumerable<AppointmentResultDto>> GetAllByPatientIdAsync(long patientId);
}
