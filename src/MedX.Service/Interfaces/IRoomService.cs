using MedX.Service.DTOs.Rooms;

namespace MedX.Service.Interfaces;

public interface IRoomService
{
    Task<RoomResultDto> AddAsync(RoomCreationDto dto);
    Task<RoomResultDto> UpdateAsync(RoomUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<RoomResultDto> GetById(long id);
    Task<IEnumerable<RoomResultDto>> GetAllAsync();
}
