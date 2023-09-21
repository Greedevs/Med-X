using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Payments;

namespace MedX.Service.DTOs.Transactions;

public class TransactionResultDto
{
    public long Id { get; set; }
    public DoctorResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
    public PaymentResultDto Payment { get; set; }
}