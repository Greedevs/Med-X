using MedX.Domain.Enums;

namespace MedX.Service.DTOs.Rooms;

public class RoomUpdateDto
{
    public long Id { get; set; }
    public int Number { get; set; }
    public int Quantity { get; set; }
    public int Available { get; set; }
    public TypeOfRoom Type { get; set; }
}