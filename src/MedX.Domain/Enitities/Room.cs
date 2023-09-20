using MedX.Domain.Commons;

namespace MedX.Domain.Enitities;

public class Room : Auditalble
{
    public int RoomNumber { get; set; }
    public int Quantity { get; set; }
}
