using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.Helpers;

public class PasswordValidator
{
    public static string Symbol { get;} = "~`! @#$%^&*()_-+={[}]|\\:;\"'<,>.?/";
    public static (bool IsValid, string Message) IsStrongPassword(string password)
    {
        if(password.Length<8) return (IsValid: false, Message: "Password can't be length 8");

        bool isUpperCase = false;
        bool isLowerCase = false;
        bool isNumberExist = false;
        bool ischarExist = false;

        foreach(var c in password)
        {
            if(char.IsUpper(c)) isUpperCase = true;
            if(char.IsLower(c)) isLowerCase = true;
            if(char.IsDigit(c)) isNumberExist = true;
            if(Symbol.Contains(c)) ischarExist = true;
        }

        if(isNumberExist == false) return (IsValid: false, Message: "Password should been at least one digit");
        if(isUpperCase == false) return (IsValid: false, Message: "Password should been at least one UpperCase");
        if(isLowerCase == false) return (IsValid: false, Message: "Password should been at least one LowerCase");
        if(ischarExist == false) return (IsValid: false, Message: "Password should been at least one Character");

        return (IsValid: true, Message: "");
    }
}
