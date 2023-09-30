using MedX.Domain.Commons;

namespace MedX.Domain.Entities.MedicalRecords;

public class MedicalRecord : Auditable
{
    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public string Deseace { get; set; }
    public string Description { get; set; }
}
