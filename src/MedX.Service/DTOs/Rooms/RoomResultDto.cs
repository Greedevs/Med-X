using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.Rooms;

public class RoomResultDto
{
    public long Id { get; set; }
    public int RoomNumber { get; set; }
    public int Quantity { get; set; }
    public ICollection<PatientResultDto> Patients { get; set; }
}