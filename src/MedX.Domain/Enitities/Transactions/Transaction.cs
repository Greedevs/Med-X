using MedX.Domain.Commons;
using MedX.Domain.Enitities.Doctors;
using MedX.Domain.Enitities.Patients;
using MedX.Domain.Enitities.Payments;

namespace MedX.Domain.Enitities;

public class Transaction : Auditable
{
    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long PaymentId { get; set; }
    public Payment Payment { get; set; }
}
