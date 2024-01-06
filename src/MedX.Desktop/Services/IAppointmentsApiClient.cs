using MedX.Service.DTOs.Appointments;

namespace MedX.Desktop.Services;

public interface IAppointmentsApiClient
{
    [Post("create")]
    Task<AppointmentResultDto> AddAsync(AppointmentCreationDto dto);

    [Put("update")]
    Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<AppointmentResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<AppointmentResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}