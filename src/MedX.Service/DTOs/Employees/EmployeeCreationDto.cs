using MedX.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedX.Service.DTOs.Employees;

public class EmployeeCreationDto
{
    //[MinLength(3), MaxLength(20), Required]
    public string FirstName { get; set; }

//    [MinLength(3), MaxLength(20), Required]
    public string LastName { get; set; }
    
  //  [MinLength(3), MaxLength(20), Required]
    public string Patronymic { get; set; }

   // [EmailAddress]
    public string Email { get; set; }
    
    //[CheckPhone, Required, Phone]
    public string Phone { get; set; }

    //[MinLength(6), MaxLength(30), Required]
    public string Password { get; set; }


    public decimal? Salary { get; set; }
    public int? Percentage { get; set; }
    public Degree Degree { get; set; }
    public string Professional { get; set; }
    public IFormFile Image { get; set; }
}