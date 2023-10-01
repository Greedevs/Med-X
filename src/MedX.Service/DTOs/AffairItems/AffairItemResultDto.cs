using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.ServiceItems;

public class AffairItemResultDto
{
    public long Id { get; set; }
    public PatientResultDto Patient { get; set; }
    public AffairItemResultDto Affair { get; set; }
    public float Quantity { get; set; }
}
