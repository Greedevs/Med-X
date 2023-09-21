using MedX.Domain.Commons;
namespace MedX.Domain.Enitities;

public class Room : Auditable
{
    public int RoomNumber { get; set; }
    public int Quantity { get; set; }

    public ICollection<Patient> Patients { get; set; }
}