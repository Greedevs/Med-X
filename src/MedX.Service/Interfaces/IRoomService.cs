using MedX.Domain.Configurations;
using MedX.Service.DTOs.Rooms;

namespace MedX.Service.Interfaces;

public interface IRoomService
{
    Task<RoomResultDto> AddAsync(RoomCreationDto dto);
    Task<RoomResultDto> UpdateAsync(RoomUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<RoomResultDto> GetAsync(long id);
    Task<IEnumerable<RoomResultDto>> GetAllAsync(PaginationParams @params, int? search = null);
}
