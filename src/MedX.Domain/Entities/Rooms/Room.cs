using MedX.Domain.Commons;
using MedX.Domain.Entities.Assets;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities;

public class Room : Auditable
{
    public int Number { get; set; }
    public int Quantity { get; set; }
    public int Busy { get; set; }
    public Gender Gender { get; set; }
    public TypeOfRoom Type { get; set; }
    public long? ImageId { get; set; }
    public Asset Image { get; set; }

    public ICollection<Patient> Patients { get; set; }
}