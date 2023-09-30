namespace MedX.Service.DTOs.ServiceItems;

public class ServiceItemCreationDto
{
    public long PatientId { get; set; }
    public long ServiceId { get; set; }
    public float Quantity { get; set; }
}