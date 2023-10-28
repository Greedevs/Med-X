using MedX.Domain.Commons;
using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities.MedicalRecords;
using MedX.Domain.Entities.Services;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities;

public class Patient : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
    public string Pinfl { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }

    public ICollection<Payment> Payments { get; set; }
    public ICollection<Treatment> Treatments { get; set; }
    public ICollection<AffairItem> AffairItems { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}
