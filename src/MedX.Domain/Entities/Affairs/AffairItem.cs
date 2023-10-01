using MedX.Domain.Commons;

namespace MedX.Domain.Entities.Services;

public class AffairItem : Auditable
{
    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long AffairId { get; set; }
    public Affair Affair { get; set; }
    
    public float Quantity { get; set; }
}