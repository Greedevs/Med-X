using MedX.Domain.Commons;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities.Administrators;

public class Administrator : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public AdminRole Role { get; set; }
    public string AccountNumber { get; set; }
}