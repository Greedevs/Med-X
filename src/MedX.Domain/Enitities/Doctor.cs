using MedX.Domain.Commons;

namespace MedX.Domain.Enitities;

public class Doctor : Auditable
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Professional { get; set;}
    public string Phone { get; set;}
    public long RoomId { get; set;}
    public Room Room { get; set;}

    public ICollection<Patient> Patients { get; set;}
    public ICollection<Transaction> Transactions { get; set;}
    public ICollection<Appointment> Appointments { get; set;}
}
