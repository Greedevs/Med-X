using MedX.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace MedX.Desktop.Models.Employees;

public class EmployeeCreationDto
{
    //[MinLength(3), MaxLength(20), Required]
    public string FirstName { get; set; } = string.Empty;

//    [MinLength(3), MaxLength(20), Required]
    public string LastName { get; set; } = string.Empty;

    //  [MinLength(3), MaxLength(20), Required]
    public string Patronymic { get; set; } = string.Empty;

    // [EmailAddress]
    public string Email { get; set; } = string.Empty;

    //[CheckPhone, Required, Phone]
    public string Phone { get; set; } = string.Empty;

    //[MinLength(6), MaxLength(30), Required]
    public string Password { get; set; } = string.Empty;

    public decimal? Salary { get; set; }
    public int? Percentage { get; set; }
    public Degree Degree { get; set; }
    public string Professional { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = default!;
}