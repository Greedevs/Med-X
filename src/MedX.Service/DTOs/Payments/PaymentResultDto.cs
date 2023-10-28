using MedX.Domain.Enums;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.Payments;

public class PaymentResultDto
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public TypeOfPayment Type { get; set; }

    public PatientResultDto Patient { get; set; }
}
