using MedX.Domain.Commons;

namespace MedX.Domain.Entities.Services;

public class Affair : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
