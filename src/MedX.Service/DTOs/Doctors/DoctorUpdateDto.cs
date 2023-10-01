using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedX.Service.DTOs.Doctors;

public class DoctorUpdateDto
{
    public long Id { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string LastName { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string Patronymic { get; set; }
    public string Professional { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    [CheckPhone, Required, Phone]
    public string Phone { get; set; }
    public int RoomNumber { get; set; }
    public IFormFile Image { get; set; }
}