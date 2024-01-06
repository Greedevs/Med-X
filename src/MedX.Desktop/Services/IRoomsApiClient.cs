using MedX.Service.DTOs.Rooms;

namespace MedX.Desktop.Services;

public interface IRoomsApiClient
{
    [Post("create")]
    Task<RoomResultDto> AddAsync(RoomCreationDto dto);

    [Put("update")]
    Task<RoomResultDto> UpdateAsync(RoomUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<RoomResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<RoomResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}