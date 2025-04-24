using MyRecipeBook.Comunication.Request;
using MyRecipeBook.Comunication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserCase
{
    public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        return new ResponseRegisterUserJson
        {
            Name = request.Name,
        };
    }


    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
