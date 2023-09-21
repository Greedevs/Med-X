using MedX.Domain.Enitities;

namespace MedX.Service.DTOs.Treatments;

public class TreatmentCreationDto
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public long RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
