using MedX.Service.DTOs.Employees;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.MedicalRecords;

public class MedicalRecordResultDto
{
    public long Id { get; set; }
    public string Disease { get; set; }
    public string Description { get; set; }
    public EmployeeResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
}