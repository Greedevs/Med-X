using MedX.Domain.Commons;
using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities.Assets;
using MedX.Domain.Entities.MedicalRecords;

namespace MedX.Domain.Entities;

public class Doctor : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Professional { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public long? ImageId { get; set; }
    public Asset Image { get; set; }

    public ICollection<Treatment> Treatments { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}