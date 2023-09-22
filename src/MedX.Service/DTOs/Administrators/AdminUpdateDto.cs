using MedX.Domain.Enums;

namespace MedX.Service.DTOs.Administrators;

public class AdminUpdateDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public AdminRole Role { get; set; }
}
