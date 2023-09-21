using MedX.Domain.Commons;

namespace MedX.Domain.Entities.Administrators;

public class Administrator : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}