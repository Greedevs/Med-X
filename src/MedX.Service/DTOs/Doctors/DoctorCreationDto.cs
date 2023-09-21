namespace MedX.Service.DTOs.Doctors;

public class DoctorCreationDto
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Professional { get; set; }
    public string Phone { get; set; }
    public long RoomId { get; set; }
}