namespace MedX.Service.DTOs.Appointments;

public class AppointmentUpdateDto
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
}
