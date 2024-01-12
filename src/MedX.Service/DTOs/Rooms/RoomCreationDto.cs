using MedX.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace MedX.Service.DTOs.Rooms;

public class RoomCreationDto
{
    public int Number { get; set; }
    public int Quantity { get; set; }
    public int Busy { get; set; } = 0;
    public TypeOfRoom Type { get; set; }
    public string Image { get; set; }
}