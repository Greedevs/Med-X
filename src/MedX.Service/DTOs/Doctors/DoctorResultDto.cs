using MedX.Service.DTOs.Rooms;

namespace MedX.Service.DTOs.Doctors;

public class DoctorResultDto
{
    public long Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Professional { get; set; }
    public string Phone { get; set; }
    public RoomResultDto Room { get; set; }
}
