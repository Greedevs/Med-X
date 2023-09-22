using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Payments;

namespace MedX.Service.DTOs.Appointments;

public class AppointmentResultDto
{
    public long Id { get; set; }
    public string Disease { get; set; }
    public DoctorResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
}