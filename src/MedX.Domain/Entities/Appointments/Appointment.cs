using MedX.Domain.Commons;

namespace MedX.Domain.Entities.Appointments;

public class Appointment : Auditable
{
    public long DoctorId { get; set; }
    public Patient Patient { get; set; }
    public long PatientId { get; set; }
    public Doctor Doctor { get; set; }
}