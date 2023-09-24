using MedX.Domain.Commons;
namespace MedX.Domain.Entities;

public class Room : Auditable
{
    public int RoomNumber { get; set; }
    public int? Quantity
    {
        get => Place;
        set => Place = value;
    }
    public int? Place { get; set; }
    public bool IsBusy { get; set; }
    public int? MaleCount { get; set; }
    public int? FemaleCount { get; set; }
    public ICollection<Patient> Patients { get; set; }
}