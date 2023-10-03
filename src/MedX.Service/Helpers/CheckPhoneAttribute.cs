using MedX.Service.Exceptions;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

public class CheckPhoneAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string phoneNumber = value.ToString() 
            ?? throw new CustomException(403, "Invalid phone number");

        string pattern = @"^\+998[0-9]{9}$";

        if (!Regex.IsMatch(phoneNumber, pattern))
            throw new CustomException(403, "Invalid phone number");

        return true;
    }
}
