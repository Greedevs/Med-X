using MedX.Domain.Commons;
using MedX.Domain.Enums;

namespace MedX.Domain.Enitities;

public class Payment : Auditable
{
    public bool IsPaid { get; set; } 
    public string PaymentFor { get; set; }
    public decimal Amount { get; set; }
    public TypeOfPayment TypeOfPayment { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}