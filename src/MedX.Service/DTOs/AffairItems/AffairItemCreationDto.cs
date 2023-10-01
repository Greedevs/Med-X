namespace MedX.Service.DTOs.ServiceItems;

public class AffairItemCreationDto
{
    public long PatientId { get; set; }
    public long AffairId { get; set; }
    public float Quantity { get; set; }
}