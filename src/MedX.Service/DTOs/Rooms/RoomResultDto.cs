using MedX.Domain.Enums;
using MedX.Service.DTOs.Patients;

namespace MedX.Service.DTOs.Rooms;

public class RoomResultDto
{
    public long Id { get; set; }
    public int Number { get; set; }
    public int Quantity { get; set; }
    public int Available { get; set; }
    public TypeOfRoom Type { get; set; }
    public ICollection<PatientResultDto> Patients { get; set; }
}