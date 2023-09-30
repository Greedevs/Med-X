using MedX.Domain.Commons;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities;

public class Room : Auditable
{
    private int busy;
    public int Number { get; set; }
    public int Quantity { get; set; }
    public int Busy { 
        get => busy;
        set
        {
            busy += busy + value > Quantity ? 0 : value;
        }
    }
    public Gender Gender { get; set; }
    public TypeOfRoom Type { get; set; }

    public ICollection<Patient> Patients { get; set; }
}