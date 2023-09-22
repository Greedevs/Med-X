namespace MedX.Service.DTOs.Doctors;

public class DoctorUpdateDto
{
    public long Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string Professional { get; set; }
    public decimal Price { get; set; }
    public string Phone { get; set; }
    public long RoomId { get; set; }
}
