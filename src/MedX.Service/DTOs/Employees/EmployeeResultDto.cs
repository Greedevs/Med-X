using MedX.Domain.Enums;
using MedX.Service.DTOs.Assets;

namespace MedX.Service.DTOs.Employees;

public class EmployeeResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Professional { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public AssetResultDto Image { get; set; }
    public decimal? Salary { get; set; }
    public int? Percentage { get; set; }
    public Degree Degree { get; set; }
}
