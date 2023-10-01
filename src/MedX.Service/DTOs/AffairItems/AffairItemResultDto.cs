using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Services;

namespace MedX.Service.DTOs.ServiceItems;

public class AffairItemResultDto
{
    public long Id { get; set; }
    public PatientResultDto Patient { get; set; }
    public AffairResultDto Affair { get; set; }
    public float Quantity { get; set; }
}
