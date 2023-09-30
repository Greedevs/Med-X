using MedX.Domain.Enums;

namespace MedX.Service.DTOs.Payments;

public class PaymentCreationDto
{
    public decimal Amount { get; set; }
    public TypeOfPayment Type { get; set; }
    public long PatientId { get; set; }
}