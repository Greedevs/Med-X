using MedX.Domain.Commons;

namespace MedX.Domain.Entities;

public class Doctor : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Professional { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
    public decimal Balance { get; set; }
    public int RoomId { get; set; }
}