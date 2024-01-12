using MedX.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedX.Service.DTOs.Administrators;

public class AdminUpdateDto
{
    public long Id { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(20), Required]
    public string LastName { get; set; }
    [CheckPhone, Required, Phone]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public AdminRole Role { get; set; }

    [MinLength(6), MaxLength(30), Required]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Image { get; set; }
}
