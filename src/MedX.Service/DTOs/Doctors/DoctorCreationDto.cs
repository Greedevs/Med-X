using System.ComponentModel.DataAnnotations;

namespace MedX.Service.DTOs.Doctors;

public class DoctorCreationDto
{
    [MinLength(3), MaxLength(20), Required]
    public string LastName { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string SurName { get; set; }
    public string Professional { get; set; }
    public decimal Price { get; set; }
    [CheckPhone, Required, Phone]
    public string Phone { get; set; }
    public long RoomId { get; set; }
}