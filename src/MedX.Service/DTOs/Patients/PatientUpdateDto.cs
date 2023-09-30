using MedX.Domain.Enums;

namespace MedX.Service.DTOs.Patients;

public class PatientUpdateDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
    public string Pinfl { get; set; }
    public decimal Balance { get; set; }
}
