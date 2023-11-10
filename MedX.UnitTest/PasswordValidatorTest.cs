using MedX.Service.Helpers;

namespace MedX.UnitTest;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("AAaa@@11")]
    [InlineData("Ass213@@11")]
    [InlineData("AAaa@#11")]
    [InlineData("AAaa@*11")]
    [InlineData("Adasr$@11")]
    [InlineData("AAawerre1&")]
    [InlineData("Aa__%)1k")]
    [InlineData("Aa__%)1.")]
    [InlineData("Aa__%)1,")]
    [InlineData("Aa__ %)1,")]
    [InlineData("Aa__ %)1,^")]
    public void IsStrongPassword(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("AAaa1")]
    [InlineData("AAaadff#$%")]
    [InlineData("aa14324")]
    [InlineData("AAASSBJH%#%&")]
    [InlineData("AAaadsfrt443")]
    [InlineData("AAaa111")]
    [InlineData("AAAAAAAA")]
    [InlineData("aaaaaaaa")]
    [InlineData("99999999999")]
    [InlineData("#%&^#%^")]
    public void ShouldReturnWeekPassword(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.False(result.IsValid);
    }
}