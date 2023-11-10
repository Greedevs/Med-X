using MedX.Service.DTOs.AffairItems;

namespace MedX.Service.DTOs.ServiceItems;

public class AffairItemCreationDto
{
    public long PatientId { get; set; }
    public ICollection<AffairItemDto> AffairItems { get; set; }
}