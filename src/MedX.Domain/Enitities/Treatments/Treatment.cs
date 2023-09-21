using MedX.Domain.Commons;
using MedX.Domain.Enitities.Doctors;
using MedX.Domain.Enitities.Patients;
using MedX.Domain.Enitities.Rooms;

namespace MedX.Domain.Enitities;

public class Treatment : Auditable
{
    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long RoomId { get; set; }
    public Room Room { get;set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
