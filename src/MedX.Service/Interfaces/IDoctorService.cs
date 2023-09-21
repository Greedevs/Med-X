using MedX.Domain.Configurations;
using MedX.Service.DTOs.Doctors;

namespace MedX.Service.Interfaces;

public interface IDoctorService
{
    Task<DoctorResultDto> AddAsync(DoctorCreationDto dto);
    Task<DoctorResultDto> UpdateAsync(DoctorUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<DoctorResultDto> GetAsync(long id);
    Task<IEnumerable<DoctorResultDto>> GetAllAsync(PaginationParams @params, string search = null);
    Task<IEnumerable<DoctorResultDto>> SearchByQueryAsync(string query);
}
