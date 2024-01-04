using MedX.Service.DTOs.Employees;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.Appointments;

public class AppointmentResultDto
{
    public long Id { get; set; }
    public EmployeeResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
}