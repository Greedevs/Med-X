using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.Appointments;

public class AppointmentResultDto
{
    public long Id { get; set; }
    public DoctorResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
}