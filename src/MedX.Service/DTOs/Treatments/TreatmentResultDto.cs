using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Rooms;

namespace MedX.Service.DTOs.Treatments;

public class TreatmentResultDto
{
    public long Id { get; set; }
    public DoctorResultDto Doctor { get; set; }
    public PatientResultDto Patient { get; set; }
    public RoomResultDto Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
