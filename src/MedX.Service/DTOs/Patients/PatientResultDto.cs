using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities.Services;
using MedX.Domain.Entities;
using MedX.Domain.Enums;
using MedX.Service.DTOs.Treatments;
using MedX.Service.DTOs.ServiceItems;
using MedX.Service.DTOs.Appointments;

namespace MedX.Service.DTOs.Patients;

public class PatientResultDto
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
    public string AccountNumber { get; set; }

    public ICollection<TreatmentResultDto> Treatments { get; set; }
    public ICollection<AffairItemResultDto> AffairItems { get; set; }
    public ICollection<AppointmentResultDto> Appointments { get; set; }
}
