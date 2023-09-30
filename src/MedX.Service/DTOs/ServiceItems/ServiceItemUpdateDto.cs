namespace MedX.Service.DTOs.ServiceItems;

public class ServiceItemUpdateDto
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public long ServiceId { get; set; }
    public float Quantity { get; set; }
}
