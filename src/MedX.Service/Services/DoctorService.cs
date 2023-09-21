using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Service.DTOs.Doctors;
using MedX.Service.Interfaces;

namespace MedX.Service.Services;

public class DoctorService : IDoctorService
{
    private readonly IRepository<>
    public Task<DoctorResultDto> AddAsync(DoctorCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DoctorResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        throw new NotImplementedException();
    }

    public Task<DoctorResultDto> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DoctorResultDto>> SearchByQuery(string query)
    {
        throw new NotImplementedException();
    }

    public Task<DoctorResultDto> UpdateAsync(DoctorUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
