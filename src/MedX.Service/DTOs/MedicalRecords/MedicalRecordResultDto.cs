using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.MedicalRecords;

public class MedicalRecordResultDto
{
    public long Id { get; set; }
    public DoctorResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
    public string Disease { get; set; }
    public string Description { get; set; }
}