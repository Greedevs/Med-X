using MedX.Domain.Enitities;

namespace MedX.Service.DTOs.Transactions;

public class TransactionCreationDto
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public long PaymentId { get; set; }
}
