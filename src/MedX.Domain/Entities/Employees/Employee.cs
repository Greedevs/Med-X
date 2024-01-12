using MedX.Domain.Commons;
using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities.MedicalRecords;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities;

public class Employee : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Professional { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public decimal? Salary { get; set; }
    public int? Percentage { get; set; }
    public Degree Degree { get; set; }
    public string Image { get; set; }

    public ICollection<Treatment> Treatments { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
}