using MedX.Domain.Commons;

namespace MedX.Domain.Entities;

public class CashDesk : Auditable
{
    public string Description { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public bool IsIncome { get; set; }
}