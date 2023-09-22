namespace MedX.Service.DTOs.Appointments;

public class AppointmentCreationDto
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public string Disease { get; set; }
}