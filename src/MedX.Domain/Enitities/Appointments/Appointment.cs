using MedX.Domain.Commons;

namespace MedX.Domain.Enitities;

public class Appointment : Auditable
{
    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long PaymentId { get; set; }
    public Payment Payment { get; set; }

    public string Disease { get; set; }
}