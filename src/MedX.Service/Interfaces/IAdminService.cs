using MedX.Domain.Configurations;
using MedX.Service.DTOs.Administrators;

namespace MedX.Service.Interfaces;

public interface IAdminService
{
    Task<AdminResultDto> AddAsync(AdminCreationDto dto);
    Task<AdminResultDto> UpdateAsync(AdminUpdateDto dto);
    /// <summary>
    /// kzsidgfuiyasbdccjzbcdiluasbcviuds
    /// </summary>
    /// <param name="id"></param>
    /// <returns>uhhzsdhbsDbhsdjhbsdfjhbds</returns>
    Task<bool> DeleteAsync(long id);
    /// <summary>
    /// dsfjhbvbddsdbcbusubdcv
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<AdminResultDto> GetAsync(long id);
    Task<IEnumerable<AdminResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}