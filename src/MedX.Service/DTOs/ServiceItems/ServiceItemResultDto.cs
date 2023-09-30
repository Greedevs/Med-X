using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.ServiceItems;

public class ServiceItemResultDto
{
    public long Id { get; set; }
    public PatientResultDto Patient { get; set; }
    public ServiceItemResultDto Service { get; set; }
    public float Quantity { get; set; }
}
