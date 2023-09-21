namespace MedX.Service.DTOs.Transactions;

public class TransactionUpdateDto
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public long PaymentId { get; set; }
}
