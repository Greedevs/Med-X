using MedX.Service.DTOs.Assets;
using MedX.Service.DTOs.Rooms;

namespace MedX.Service.DTOs.Doctors;

public class DoctorResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Professional { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public int RoomNumber { get; set; }
    public string AccountNumber { get; set; }
    public AssetResultDto Image { get; set; }
}
