using MedX.Domain.Commons;
using MedX.Domain.Enums;

namespace MedX.Domain.Enitities;

public class Patient : Auditalble
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
    public string Pinfl { get; set; }
}
