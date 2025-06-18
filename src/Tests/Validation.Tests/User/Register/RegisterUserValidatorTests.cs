using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;

namespace Validation.Tests.User.Register;
public class RegisterUserValidatorTests
{
    [Fact]
    public void Sucess()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}