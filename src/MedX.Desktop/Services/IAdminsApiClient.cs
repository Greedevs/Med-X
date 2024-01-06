using MedX.Desktop.Models;
using MedX.Service.DTOs.Administrators;

namespace MedX.Desktop.Services;

public interface IAdminsApiClient
{
    [Post("create")]
    Task<Response<AdminResultDto>> AddAsync(AdminCreationDto dto);

    [Put("update")]
    Task<Response<AdminResultDto>> UpdateAsync(AdminUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<Response<AdminResultDto>> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<Response<IEnumerable<AdminResultDto>>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}