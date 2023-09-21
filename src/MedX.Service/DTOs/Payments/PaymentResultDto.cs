using MedX.Domain.Enums;

namespace MedX.Service.DTOs.Payments;

public class PaymentResultDto
{
    public long Id { get; set; }
    public bool IsPaid { get; set; }
    public decimal Amount { get; set; }
    public string PaymentFor { get; set; }
    public TypeOfPayment TypeOfPayment { get; set; }
}
