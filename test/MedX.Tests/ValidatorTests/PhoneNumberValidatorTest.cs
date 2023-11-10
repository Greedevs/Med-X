using MedX.Service.Validators;

namespace MedX.Tests.ValidatorTests;

public class PhoneNumberValidatorTest
{
    [Theory]
    [InlineData("+99937349808")]
    [InlineData("+9989373498O8")]
    [InlineData("+998927349808")]
    [InlineData("998937349808")]
    public void ShoulReturnFalse(string phoneNumber)
    {
        var validator = new PhoneNumberValidator();
        var result = validator.IsValid(phoneNumber);
        Assert.False(result);
    }


    [Theory]
    [InlineData("+998937349808")]
    public void ShoulReturnTrue(string phoneNumber)
    {
        var validator = new PhoneNumberValidator();
        var result = validator.IsValid(phoneNumber);
        Assert.True(result);
    }
}