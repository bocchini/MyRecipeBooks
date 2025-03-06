using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Comunication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserUseCase
{
    public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
    {

        return new ResponseRegisterUserJson
        {
            Nome = request.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false) 
        {
            var erroMessage = result.Errors.Select(e => e.ErrorMessage);
            throw new ErrorOnValidationExcetpion(erroMessage.ToList());
        }
    }
}
