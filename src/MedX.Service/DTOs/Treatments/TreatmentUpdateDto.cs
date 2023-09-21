namespace MedX.Service.DTOs.Treatments;

public class TreatmentUpdateDto
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public long RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
