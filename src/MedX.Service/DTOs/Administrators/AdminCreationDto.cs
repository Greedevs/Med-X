using MedX.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MedX.Service.DTOs.Administrators;

public class AdminCreationDto
{
    [MinLength(3), MaxLength(20), Required]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string LastName { get; set; }
    [CheckPhone, Required, Phone]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public AdminRole Role { get; set; }
}