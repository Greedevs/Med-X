namespace MedX.Service.DTOs.MedicalRecords;

public class MedicalRecordUpdateDto
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public string Disease { get; set; }
    public string Description { get; set; }
}
