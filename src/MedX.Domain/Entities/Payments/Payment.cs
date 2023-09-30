using MedX.Domain.Commons;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities;

public class Payment : Auditable
{
    public decimal Amount { get; set; }
    public TypeOfPayment Type { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }
}