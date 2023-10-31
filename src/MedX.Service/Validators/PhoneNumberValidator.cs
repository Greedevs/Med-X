using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MedX.Service.Validators;

public class PhoneNumberValidator : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string phoneNumber = (string)value ?? "";
        string pattern = @"^\+998(90|91|93|94|97|50|88|20|33|70|99)[0-9]{7}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}