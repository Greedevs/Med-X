using MedX.Domain.Enums;

namespace MedX.Service.DTOs.Administrators;

public class AdminResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public AdminRole Role { get; set; }
    public string AccountNumber { get; set; }
    public string Image { get; set; }
}
