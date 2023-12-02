using MedX.Domain.Commons;

namespace MedX.Domain.Entities.MedicalRecords;

public class MedicalRecord : Auditable
{
    public string Disease { get; set; }
    public string Description { get; set; }
    public long DoctorId { get; set; }
    public Employee Doctor { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }
}
