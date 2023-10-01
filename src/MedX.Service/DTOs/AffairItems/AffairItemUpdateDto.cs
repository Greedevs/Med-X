namespace MedX.Service.DTOs.ServiceItems;

public class AffairItemUpdateDto
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public long AffairId { get; set; }
    public float Quantity { get; set; }
}
