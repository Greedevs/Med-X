using MedX.Domain.Commons;
using MedX.Domain.Enums;

namespace MedX.Domain.Entities;

public class Patient : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
    public string? Pinfl { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
}
