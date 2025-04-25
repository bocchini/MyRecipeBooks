using MyRecipeBook.Comunication.Request;
using MyRecipeBook.Comunication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptography;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserCase
{
    public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var passwordCriptografado = new PasswordEncripter();

        var automapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();
               
        var user = automapper.Map<Domain.Entities.User>(request);
        user.Password = passwordCriptografado.Encrypt(request.Password);

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
