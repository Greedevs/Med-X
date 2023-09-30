using MedX.Domain.Commons;

namespace MedX.Domain.Entities.Services;

public class ServiceItem : Auditable
{
    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long ServiceId { get; set; }
    public Service Service { get; set; }
    
    public float Quantity { get; set; }
}
