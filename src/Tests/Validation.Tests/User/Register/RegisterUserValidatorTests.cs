using MyRecipeBook.Application.UseCases.User.Register;

namespace Validation.Tests.User.Register;
public class RegisterUserValidatorTests
{
    [Fact]
    public void Sucess()
    {
        var validator = new RegisterUserValidator();

        var request = new MyRecipeBook.Comunication.Request.RequestRegisterUserJson
        {
            Email = "email@gamil.com"
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}